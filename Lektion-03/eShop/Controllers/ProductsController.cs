using eShop.Data;
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
        var products = await context.Products.ToListAsync();
        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Items = products.Count,
            Data = products
        });
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return StatusCode(201);
    }
}

