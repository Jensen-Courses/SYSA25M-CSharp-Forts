using api.DTOs.Orders;
using api.Extensions;
using core.Entities;
using core.Entities.Orders;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
public class OrdersController(ICartService cartService, IUnitOfWork uow) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateOrder(PostOrderDto orderDto)
    {
        // 1.   Hämta inloggad användares e-post adress...
        var email = User.GetUserEmail();

        // 2.   Hämta in kundkorg...
        var cart = await cartService.GetCartAsync(orderDto.CartId);
        if (cart is null) return BadRequest("Ingen kundkorg kunde hittas");

        // 3.   Gå igenom alla produkter i cart(kundkorgen) cart.items...
        var items = new List<OrderItem>();

        foreach (var item in cart.Items)
        {
            // 3.1 Leta upp produkten i databasen...
            var product = await uow.Repository<Product>().FindByIdAsync(item.ProductId);
            if (product is null) return BadRequest("Problem med beställning");

            // 3.2 Skapa ett ItemOrdered objekt för produkten
            var productItem = new ItemOrdered
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName
            };

            // 3.3  Skapa ett OrderItem objekt för beställning
            var orderItem = new OrderItem
            {
                ItemOrdered = productItem,
                Price = product.Price,
                Quantity = item.Quantity
            };

            // 3.4 Lägg till objektet till listan items...
            items.Add(orderItem);
        }

        // 4.   Hämta leveransmetoden(sättet) ifrån databasen...
        var deliveryMethod = await uow.Repository<DeliveryMethod>().FindByIdAsync(orderDto.DeliveryMethodId);
        if (deliveryMethod is null) return BadRequest("Leveranssätt saknas eller är felaktigt!");

        // 5.   Skapa beställningen...
        var order = new Order
        {
            OrderItems = items,
            DeliveryMethod = deliveryMethod,
            ShippingAddress = orderDto.ShippingAddress,
            PaymentInfo = orderDto.PaymentInfo,
            CustomerEmail = email,
            SubTotal = items.Sum(c => c.Price * c.Quantity)
        };

        uow.Repository<Order>().Add(order);

        if (await uow.Complete()) return StatusCode(201, order);

        return BadRequest("Det gick inte att slutföra beställningen");
    }

    [HttpGet()]
    public async Task<ActionResult> GetOrdersForUser()
    {
        var spec = new OrderSpecification(User.GetUserEmail());
        var orders = await uow.Repository<Order>().ListAsync(spec);

        var result = orders.Select(c => c.ToDTO()).ToList();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrderById(string id)
    {
        var spec = new OrderSpecification(User.GetUserEmail(), id);
        var order = await uow.Repository<Order>().FindAsync(spec);
        if (order is null) return NotFound();
        return Ok(order.ToDTO());
    }
}
