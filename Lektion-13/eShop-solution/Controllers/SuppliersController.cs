using eShop.DTOs.Suppliers;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(IGenericRepository<Supplier> repo) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        try
        {
            var suppliers = await repo.ListAllAsync();
            return Ok(new { Success = true, StatusCode = 200, Items = suppliers.Count, Data = suppliers });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Sorry, något gick fel {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        try
        {
            var supplier = await repo.FindByIdAsync(id);
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = supplier });
        }
        catch
        {
            return NotFound("Hittade inget");
        }
    }

    // [HttpPost()]
    // public async Task<ActionResult> AddSupplier(PostSupplierDto supplier)
    // {
    //     try
    //     {
    //         if (await uow.SupplierRepository.AddSupplier(supplier))
    //         {
    //             await uow.Complete();
    //             return StatusCode(201, supplier);
    //         }

    //         return StatusCode(500, "Något gick när vi skulle spara ny leverantör");
    //     }
    //     catch
    //     {
    //         return StatusCode(500, "Något gick när vi skulle spara ny leverantör");
    //     }
    // }
}

