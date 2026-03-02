using eShop.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("sqlitedev"));
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
