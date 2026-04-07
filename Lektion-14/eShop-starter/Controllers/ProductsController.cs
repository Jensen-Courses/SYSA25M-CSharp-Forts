using AutoMapper;
using eShop.DTOs.Products;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IGenericRepository<Product> repo, IMapper mapper) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        try
        {
            var products = await repo.ListAllAsync();
            var productsDto = mapper.Map<IList<GetProductsDto>>(products);
            return Ok(new { Success = true, StatusCode = 200, Items = products.Count, Data = productsDto });
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
            var product = await repo.FindByIdAsync(id);
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade, vi kan tyvärr inte hitta produkten.");
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto model)
    {
        try
        {
            var product = mapper.Map<Product>(model);

            repo.Add(product);

            if (await repo.SaveAllAsync())
            {
                return StatusCode(201);
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
            var product = await repo.FindAsync(c => c.ItemNumber == itemNumber);

            if (product is null) return NotFound();

            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade, vi kan tyvärr inte hitta produkten.");
        }
    }



    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        try
        {
            product.Id = id;
            repo.Update(product);

            if (await repo.SaveAllAsync())
            {
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
            var product = await repo.FindByIdAsync(id);
            if (product is null) return BadRequest("Hittade ingen product");

            repo.Delete(product);

            if (await repo.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Ett server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Ett server fel inträffade");
        }

    }

}

