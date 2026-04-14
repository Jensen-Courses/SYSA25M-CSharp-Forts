using api.Helpers;
using core.Interfaces;
using infrastructure.Data;
using infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});

// DI...
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAutoMapper(options =>
{
    options.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA2NjI0MDAwIiwiaWF0IjoiMTc3NTEzNjIzNSIsImFjY291bnRfaWQiOiIwMTlkNGU1YzdhOTk3MjdlOTE2MTczNjFmNmY5NWI1OSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa243NXRyYnBucDZqd3Q1eGI0djFqMmszIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.rUZpqAO2TlP5y9L-6Fo8ZlGtS2RCUEL9UVU5ySS2E_QMTYNVRf50j2ueBYLD6FyYu55PNDq2BfhquoIrwn2_FXJO9W2W-lB57vqkjun0RNROrTcSb4bFW-YB9cgrqU3QkYhCOXe7CaUILPnv8pNY4rpKS8N-iuymuABrTVan7a7L5FYLArBqIjvzSB0pvdWJlu4t0cX97JFfRIawSbFmA343aVKZCQUMg0xPlYflXhRk65UBGZzR3Qu0rBTXVUCWnTmyUvsrUpCHbvnw71AkIohzbe_UynhTdd3Sb4VT6pXAOYgwHcQVT2P-wTDbGUh3rwXBAFy6lgsgLJCF2w6yeA";
    options.AddProfile(new MappingProfiles());
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

// Seed database...
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EShopContext>();
    await context.Database.MigrateAsync();
    await SeedDatabase.SeedProducts(context);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
