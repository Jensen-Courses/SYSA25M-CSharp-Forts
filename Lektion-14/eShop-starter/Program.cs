using eShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using eShop.Entities;
using System.Text;
using eShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using eShop.Interfaces;
using eShop.Repositories;
using eShop;
using eShop.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
    // options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
});

builder.Services.AddAutoMapper(options =>
{
    options.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA2NjI0MDAwIiwiaWF0IjoiMTc3NTEzNjIzNSIsImFjY291bnRfaWQiOiIwMTlkNGU1YzdhOTk3MjdlOTE2MTczNjFmNmY5NWI1OSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa243NXRyYnBucDZqd3Q1eGI0djFqMmszIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.rUZpqAO2TlP5y9L-6Fo8ZlGtS2RCUEL9UVU5ySS2E_QMTYNVRf50j2ueBYLD6FyYu55PNDq2BfhquoIrwn2_FXJO9W2W-lB57vqkjun0RNROrTcSb4bFW-YB9cgrqU3QkYhCOXe7CaUILPnv8pNY4rpKS8N-iuymuABrTVan7a7L5FYLArBqIjvzSB0pvdWJlu4t0cX97JFfRIawSbFmA343aVKZCQUMg0xPlYflXhRk65UBGZzR3Qu0rBTXVUCWnTmyUvsrUpCHbvnw71AkIohzbe_UynhTdd3Sb4VT6pXAOYgwHcQVT2P-wTDbGUh3rwXBAFy6lgsgLJCF2w6yeA";
    options.AddProfile(new MappingProfiles());
});

// 1. Lägg till inloggningsinställningar (authentication)...
builder.Services.AddIdentityCore<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EShopContext>();

// Depency Injection...
// Registera vår TokenServer i dotnet's dependency lista...
builder.Services.AddScoped<TokenService>();
// Registrera övriga tjänster...
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// builder.Services.AddControllers(options =>
// {
//     // Skapa en generell regel(policy) som tvingar alla att vara inloggade...
//     var policy = new AuthorizationPolicyBuilder()
//         .RequireAuthenticatedUser()
//         .Build();

//     // Applicera regeln...
//     options.Filters.Add(new AuthorizeFilter(policy));

// });

// 2. Aktivera ett auktoriserings schema, dvs hur ska vi kontrollera användaren...
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("tokenSettings:tokenKey").Value))
        };
    });

// 6. Ett alternativt sätt att koppla behörighet i dotnet...
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireCorporateRights", policy => policy.RequireRole("Admin", "Manager"));

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdminRights", policy => policy.RequireRole("Admin"));

// 3. Aktivera behörighetskontroll...
builder.Services.AddAuthorization();

var app = builder.Build();

// 4. Använd användar inloggning i systemet...
app.UseAuthentication();

// 5. Använda behörighetskontroll...
app.UseAuthorization();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var seed = new SeedDatabase(userManager, roleManager);
    await seed.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
