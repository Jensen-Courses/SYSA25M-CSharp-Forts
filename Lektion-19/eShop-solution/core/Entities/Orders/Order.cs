namespace core.Entities.Orders;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public required string CustomerEmail { get; set; }
    public ShippingAddress ShippingAddress { get; set; } = null!;
    public PaymentInfo PaymentInfo { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = [];
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public double SubTotal { get; set; }

    public double GetTotalAmount()
    {
        return SubTotal + DeliveryMethod.Price; //moms osv...
    }
}
