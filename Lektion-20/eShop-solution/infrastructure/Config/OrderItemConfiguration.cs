using core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(c => c.Price).HasColumnType("decimal(18,2)");
        builder.OwnsOne(c => c.ItemOrdered, i => i.WithOwner());
    }
}
