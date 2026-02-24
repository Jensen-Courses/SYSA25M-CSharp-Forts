using eShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data;

public class EShopContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
