using eShop.Data;
using eShop.DTOs;
using eShop.DTOs.Products;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    // [AllowAnonymous]
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await repo.ListAllProducts();
        return Ok(new { Success = true, StatusCode = 200, Items = products.Count, Data = products });
    }

    // [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        try
        {
            var product = await repo.FindProduct(id);
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto product)
    {
        try
        {
            var id = await repo.AddProduct(product);
            return CreatedAtAction(nameof(FindProduct), new { id }, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    // [AllowAnonymous]
    // [HttpGet("product/{itemNumber}")]
    // public async Task<ActionResult> FindProduct(string itemNumber)
    // {
    //     Product product = await context.Products.SingleOrDefaultAsync(p => p.ItemNumber == itemNumber);
    //     if (product is null) return NotFound();

    //     return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
    // }



    // [HttpPut("{id}")]
    // public async Task<ActionResult> UpdateProduct(int id, Product product)
    // {
    //     Product productToUpdate = await context.Products.FindAsync(id);
    //     if (productToUpdate is null) return NotFound();

    //     productToUpdate.ItemNumber = product.ItemNumber;
    //     productToUpdate.Description = product.Description;
    //     productToUpdate.Price = product.Price;
    //     productToUpdate.ProductName = product.ProductName;
    //     productToUpdate.ImageUrl = product.ImageUrl;

    //     await context.SaveChangesAsync();

    //     return NoContent();
    // }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> RemoveProduct(int id)
    // {
    //     Product product = await context.Products.FindAsync(id);
    //     if (product is not null)
    //     {
    //         context.Products.Remove(product);
    //         await context.SaveChangesAsync();
    //     }

    //     return NoContent();
    // }

    // [HttpPatch("{id}")]
    // public async Task<ActionResult> PatchProduct(int id, Product product)
    // {
    //     Product productToPatch = await context.Products.FindAsync(id);
    //     if (productToPatch is null) return NotFound();

    //     productToPatch.Price = product.Price;

    //     await context.SaveChangesAsync();
    //     return NoContent();
    // }
}

