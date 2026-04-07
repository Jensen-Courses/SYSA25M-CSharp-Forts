using eShop.DTOs.Products;
using eShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IUnitOfWork uow) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        try
        {
            var products = await uow.ProductRepository.ListAllProducts();
            return Ok(new { Success = true, StatusCode = 200, Items = products.Count, Data = products });
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        try
        {
            var product = await uow.ProductRepository.FindProduct(id);
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade, vi kan tyvärr inte hitta produkten.");
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto product)
    {
        try
        {
            if (await uow.ProductRepository.AddProduct(product))
            {
                await uow.Complete();
                return StatusCode(201, product);
            }

            return StatusCode(500, "Något server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade");
        }

    }

    [HttpGet("product/{itemNumber}")]
    public async Task<ActionResult> FindProduct(string itemNumber)
    {
        try
        {
            var product = await uow.ProductRepository.FindProduct(itemNumber);
            if (product is null) return NotFound();

            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade, vi kan tyvärr inte hitta produkten.");
        }
    }



    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, PutProductDto product)
    {
        try
        {
            if (await uow.ProductRepository.UpdateProduct(id, product))
            {
                await uow.Complete();
                return NoContent();
            }
            return StatusCode(500, "Något server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade");
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveProduct(int id)
    {
        try
        {
            if (await uow.ProductRepository.DeleteProduct(id))
            {
                await uow.Complete();
                return NoContent();
            }
            return StatusCode(500, "Ett server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Ett server fel inträffade");
        }

    }

}

