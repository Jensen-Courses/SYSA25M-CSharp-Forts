using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Entities;

public class Product : BaseEntity
{
    public int SupplierId { get; set; }
    public required string ItemNumber { get; set; }
    public required string ProductName { get; set; }
    public required decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    [ForeignKey("SupplierId")]
    public Supplier Supplier { get; set; }
}
