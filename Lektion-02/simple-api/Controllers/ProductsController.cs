using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using simple_api.Data;
using simple_api.Models;

namespace simple_api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _path;
    public ProductsController(IWebHostEnvironment environment)
    {
        _environment = environment;
        _path = string.Concat(_environment.ContentRootPath, "/Data/products.json");
    }

    [HttpGet()]
    public ActionResult ListProducts()
    {
        var products = Storage<Product>.ReadJson(_path);
        return Ok(new
        {
            Success = true,
            statusCode = "200",
            items = products.Count,
            Data = products
        });
    }

    [HttpGet("{id}")]
    public ActionResult FindProduct(int id)
    {
        var products = Storage<Product>.ReadJson(_path);
        Product product = products.SingleOrDefault(c => c.Id == id);

        if (product is null) return NotFound(new
        {
            Success = false,
            StatusCode = 404,
            Message = "Hittade inget!"
        });

        return Ok(new
        {
            Success = true,
            statusCode = "200",
            Data = product
        });
    }

    [HttpPost()]
    public ActionResult AddProduct(Product product)
    {
        var products = Storage<Product>.ReadJson(_path);
        products.Add(product);
        Storage<Product>.WriteJson(_path, products);
        return CreatedAtAction(nameof(FindProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateProduct(int id, Product product)
    {
        // Logik som uppdaterar en produkt...
        var products = Storage<Product>.ReadJson(_path);
        var item = products.SingleOrDefault(c => c.Id == id);

        if (item is null) return NotFound("Hittar inte!");

        products.Remove(item);
        products.Add(product);
        Storage<Product>.WriteJson(_path, products);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var products = Storage<Product>.ReadJson(_path);
        var item = products.SingleOrDefault(c => c.Id == id);
        products.Remove(item);
        Storage<Product>.WriteJson(_path, products);
        return NoContent();
    }

}

