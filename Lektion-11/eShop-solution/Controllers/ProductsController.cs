using eShop.DTOs.Products;
using eShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await repo.ListAllProducts();
        return Ok(new { Success = true, StatusCode = 200, Items = products.Count, Data = products });
    }

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

    [HttpGet("product/{itemNumber}")]
    public async Task<ActionResult> FindProduct(string itemNumber)
    {
        try
        {
            var product = await repo.FindProduct(itemNumber);
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }



    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, PutProductDto product)
    {
        try
        {
            if (await repo.UpdateProduct(id, product)) return NoContent();

            return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveProduct(int id)
    {
        try
        {
            await repo.DeleteProduct(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

