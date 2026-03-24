using eShop.Data;
using eShop.DTOs.Customers;
using eShop.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomersController(EShopContext context, UserManager<User> userManager) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllCustomers()
    {
        var items = await context.Customers.ToListAsync();

        List<GetAllCustomersDto> customers = [.. items
            .Select(c => new GetAllCustomersDto()
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName
            })];

        return Ok(new { Success = true, StatusCode = 200, Items = customers.Count, Data = customers });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindCustomer(int id)
    {
        Customer customer = await context.Customers.FindAsync(id);
        if (customer is null) return NotFound(new { Message = "Hittade ingen kund" });

        GetCustomerDto dto = new()
        {
            CustomerName = customer.FirstName + " " + customer.LastName,
            Email = customer.Email
        };

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = dto });
    }

    [HttpGet("{id}/cart")]
    public async Task<ActionResult> ListCartContent(string id)
    {
        // Vem är den inloggade användaren?
        var user = User;
        var email = user.Identity.Name;

        // Vem tillhör varukorgen?
        var customer = await userManager.FindByIdAsync(id);

        if (email != customer.Email) return BadRequest();

        return Ok("Det är min kundkorg");

        // Cart cart = await context.Carts
        //     .Include(c => c.Customer)
        //     .Include(c => c.CartItems)
        //     .ThenInclude(c => c.Product)
        //     .SingleOrDefaultAsync(c => c.CustomerId == id);

        // var data = new
        // {
        //     CreatedDate = cart.CreatedDate.ToShortDateString(),
        //     cart.Customer.FirstName,
        //     cart.Customer.LastName,
        //     cart.Customer.Email,
        //     Total = cart.CartItems.Sum(p => p.Price * p.Quantity),
        //     Items = cart.CartItems.Select(c => new
        //     {
        //         c.ProductId,
        //         c.Product.ProductName,
        //         c.Price,
        //         c.Quantity,
        //         LineSum = c.Price * c.Quantity
        //     })
        // };

        // return Ok(new
        // {
        //     Success = true,
        //     StatusCode = 200,
        //     Items = cart.CartItems.Count,
        //     Data = data
        // });



    }

    [HttpPost()]
    public async Task<ActionResult> AddCustomer(PostCustomerDto model)
    {
        Customer customer = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        return StatusCode(201, customer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, PutCustomerDto model)
    {
        Customer customer = await context.Customers.FindAsync(id);
        if (customer is null) return NotFound(new { Message = "Hittade ingen kund" });

        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.Email = model.Email;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        Customer customer = await context.Customers.FindAsync(id);

        if (customer is not null)
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }

        return NoContent();
    }
}

