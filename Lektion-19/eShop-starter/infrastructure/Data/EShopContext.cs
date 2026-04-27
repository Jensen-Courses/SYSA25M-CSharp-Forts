using core.Entities;
using core.Entities.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class EShopContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Konfigurera Order entiteten...
        builder.Entity<Order>().OwnsOne(c => c.ShippingAddress);
        builder.Entity<Order>().OwnsOne(c => c.PaymentInfo);
        builder.Entity<Order>().HasMany(c => c.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Order>().Property(c => c.Status).HasConversion(o => o.ToString(), o => Enum.Parse<OrderStatus>(o));
        builder.Entity<Order>().Property(c => c.OrderDate).HasConversion(
            c => c.ToUniversalTime(),
            c => DateTime.SpecifyKind(c, DateTimeKind.Utc)
        );

        // Konfigurera OrderItem entiteten...
        builder.Entity<OrderItem>().OwnsOne(c => c.ItemOrdered, i => i.WithOwner());
    }
}
