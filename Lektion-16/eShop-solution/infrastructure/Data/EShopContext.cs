using core.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class EShopContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
