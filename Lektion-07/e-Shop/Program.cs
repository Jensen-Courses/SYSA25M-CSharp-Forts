using eShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using eShop.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("sqlitedev"));
});

// 1. Lägg till inloggningsinställningar (authentication)...
builder.Services.AddIdentityCore<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EShopContext>();

builder.Services.AddControllers();

// 2. Aktivera ett auktoriserings schema, dvs hur ska vi kontrollera användaren...
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

// 3. Aktivera behörighetskontroll...
builder.Services.AddAuthorization();

var app = builder.Build();

// 4. Använd användar inloggning i systemet...
app.UseAuthentication();

// 5. Använda behörighetskontroll...
app.UseAuthorization();

app.MapControllers();

app.Run();
