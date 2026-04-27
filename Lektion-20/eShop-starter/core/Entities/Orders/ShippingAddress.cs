namespace core.Entities.Orders;

public class ShippingAddress
{
    public required string Name { get; set; }
    public required string AddressLine { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }
}