using infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
