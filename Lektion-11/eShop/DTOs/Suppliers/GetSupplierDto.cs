namespace eShop.DTOs.Suppliers;

public class GetSupplierDto : GetSuppliersDto
{
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}
