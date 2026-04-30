using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(c => c.Price).HasColumnType("decimal(18,2)");
        builder.Property(c => c.ProductName).IsRequired().HasMaxLength(100);
    }
}
