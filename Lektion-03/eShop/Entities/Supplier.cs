using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace eShop.Entities;

public record Supplier
{
    [Key]
    public int SupplierNumber { get; set; }
    [NotNull]
    public string SupplierName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    [NotNull]
    public string Phone { get; set; }
    [NotNull]
    public string Email { get; set; }
    // Navigeringsenskap
    public List<Product> Products { get; set; }
}
