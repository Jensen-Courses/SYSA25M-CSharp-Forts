using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Entities;

public class Cart
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Navigeringsegenskaper...
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
    public List<CartItem> CartItems { get; set; }
}
