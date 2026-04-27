namespace core.Entities;

public class Address : BaseEntity
{
    public required string AddressLine { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }
}
