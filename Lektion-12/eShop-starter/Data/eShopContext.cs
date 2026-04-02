using eShop.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data;

public class EShopContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<SalesOrder> SalesOrders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Skapat två kolumner som sammansatt primary key...
        modelBuilder.Entity<CartItem>().HasKey(c => new { c.CartId, c.ProductId });
        modelBuilder.Entity<OrderItem>().HasKey(c => new { c.ProductId, c.SalesOrderId });
        base.OnModelCreating(modelBuilder);
    }
}
