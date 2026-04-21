namespace api.DTOs.Suppliers;

public class GetSupplierDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? WebSite { get; set; }
    public int CustomerNumber { get; set; }
}
