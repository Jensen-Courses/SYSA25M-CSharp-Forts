using eShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data;

public class EShopContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Skapat två kolumner som sammansatt primary key...
        modelBuilder.Entity<CartItem>().HasKey(c => new { c.CartId, c.ProductId });
        base.OnModelCreating(modelBuilder);
    }
}
