using core.Entities.Orders;

namespace api.Extensions;

public static class OrderMappingExtensions
{
    public static OrderDto ToDTO(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerEmail = order.CustomerEmail,
            ShippingAddress = order.ShippingAddress,
            PaymentInfo = order.PaymentInfo,
            DeliveryMethod = order.DeliveryMethod.Description + " " + order.DeliveryMethod.DeliveryTime,
            OrderItems = [.. order.OrderItems.Select(c => c.ToDTO())],
            SubTotal = order.SubTotal,
            Status = order.Status.ToString()
        };
    }

    public static OrderItemDto ToDTO(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            ProductId = orderItem.ItemOrdered.ProductId,
            ProductName = orderItem.ItemOrdered.ProductName,
            Price = orderItem.Price,
            Quantity = orderItem.Quantity
        };
    }
}


public class OrderDto
{
    public required string Id { get; set; }
    public DateTime OrderDate { get; set; }
    public required string CustomerEmail { get; set; }
    public required ShippingAddress ShippingAddress { get; set; }
    public required string DeliveryMethod { get; set; }
    public decimal ShippingPrice { get; set; }
    public required PaymentInfo PaymentInfo { get; set; }
    public required List<OrderItemDto> OrderItems { get; set; }
    public decimal SubTotal { get; set; }
    public required string Status { get; set; }
}

public class OrderItemDto
{
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}