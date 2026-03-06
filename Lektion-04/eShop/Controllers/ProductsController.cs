using eShop.Data;
using eShop.DTOs;
using eShop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(EShopContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        // Projecering, bara returnera det som behövs i frontend...
        var products = await context.Products
            .Select(product => new
            {
                product.Id,
                product.ProductName,
                product.ImageUrl
            })
            .ToListAsync();

        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Items = products.Count,
            Data = products
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await context.Products
            .Where(c => c.Id == id)
            .Select(p => new
            {
                p.ItemNumber,
                p.Description,
                p.Price,
                p.ProductName,
                p.ImageUrl
            })
            .SingleOrDefaultAsync();
        if (product is not null)
        {
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }

        return NotFound();
    }

    // http://localhost:5001/api/products/search/itemnumber
    [HttpGet("product/{itemNumber}")]
    public async Task<ActionResult> FindProduct(string itemNumber)
    {
        Product product = await context.Products.SingleOrDefaultAsync(p => p.ItemNumber == itemNumber);
        if (product is null) return NotFound();

        return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto product)
    {
        // 1. Hämta leverantören av produkten...
        Supplier supplier = await context.Suppliers.FindAsync(product.SupplierId);
        if (supplier is null) return NotFound();

        // 2. Skapa en ny produkt baserat på data ifrån PostProductDto objektet...
        Product item = new()
        {
            ItemNumber = product.ItemNumber,
            ProductName = product.ProductName,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Description = product.Description,
            Supplier = supplier
        };

        context.Products.Add(item);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(FindProduct), new { id = item.Id }, product);
    }

    /*
        PUT är till för att uppdatera hela objektet på servern...
    */
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        // Hämta produkten som ska uppdateras
        Product productToUpdate = await context.Products.FindAsync(id);
        if (productToUpdate is null) return NotFound();

        // Sätt nya värden till produkten
        productToUpdate.ItemNumber = product.ItemNumber;
        productToUpdate.Description = product.Description;
        productToUpdate.Price = product.Price;
        productToUpdate.ProductName = product.ProductName;
        productToUpdate.ImageUrl = product.ImageUrl;

        // Spara ner förändringen till databasen
        await context.SaveChangesAsync();

        // Returnera ingenting
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveProduct(int id)
    {
        Product product = await context.Products.FindAsync(id);
        if (product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        return NoContent();
    }

    /* 
        PATCH används för att uppdatera delar av ett objekt på servern...
    */
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchProduct(int id, Product product)
    {
        Product productToPatch = await context.Products.FindAsync(id);
        if (productToPatch is null) return NotFound();

        productToPatch.Price = product.Price;

        await context.SaveChangesAsync();
        return NoContent();
    }
}

