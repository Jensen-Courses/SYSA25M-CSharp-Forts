using System.Diagnostics.CodeAnalysis;

namespace eShop.Entities;

public record Product
{
    public int Id { get; set; }
    [NotNull]
    public string ItemNumber { get; set; }
    [NotNull]
    public string ProductName { get; set; }
    [NotNull]
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    // Navigeringsegenskap låter oss hämta
    // vilken leverantör äger produkten
    public Supplier Supplier { get; set; }
}
