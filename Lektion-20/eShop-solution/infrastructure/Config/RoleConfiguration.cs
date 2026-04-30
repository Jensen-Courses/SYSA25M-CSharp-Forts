using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Config;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole { Id = "284d73ab-debe-481e-9a22-b9c3d64e7651", ConcurrencyStamp = "admin", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "594c4339-d3c1-4ea4-8089-b1e4245099ad", ConcurrencyStamp = "customer", Name = "Customer", NormalizedName = "CUSTOMER" }
        );
    }
}
