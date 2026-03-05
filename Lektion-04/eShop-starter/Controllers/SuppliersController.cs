using eShop.Data;
using eShop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(EShopContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        List<Supplier> suppliers = await context.Suppliers.ToListAsync();
        return Ok(new { Success = true, StatusCode = 200, Items = suppliers.Count, Data = suppliers });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        Supplier supplier = await context.Suppliers
            // Gör en join med produkt tabellen (två tabells join)... 
            .Include(s => s.Products)
            .SingleOrDefaultAsync(s => s.SupplierNumber == id);
        if (supplier is null) return NotFound();

        var supplierToReturn = new
        {
            supplier.SupplierNumber,
            supplier.SupplierName,
            Products = supplier.Products.Select(p => new
            {
                p.ItemNumber,
                p.ProductName,
                p.Price
            })

        };
        return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = supplierToReturn });
    }

    [HttpPost()]
    public async Task<ActionResult> AddSupplier(Supplier supplier)
    {
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(FindSupplier), new { id = supplier.SupplierNumber }, supplier);
    }
}

