using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class SuppliersController(IGenericRepository<Supplier> repo, IMapper mapper) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> ListAllSuppliers([FromQuery] SupplierSpecificationParams args)
    {
        var spec = new SupplierSpecification(args);
        return await CreatePagedResult(repo, spec, args.PageNumber, args.PageSize);
    }

    [HttpPost]
    public async Task<ActionResult> AddSupplier(PostSupplierDto model)
    {
        var supplier = mapper.Map<Supplier>(model);
        repo.Add(supplier);

        if (await repo.SaveAllAsync())
        {
            return StatusCode(201);
        }

        return StatusCode(500, "Något server fel inträffade");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSupplier(string id, PostSupplierDto model)
    {
        var supplier = mapper.Map<Supplier>(model);
        supplier.Id = id;
        repo.Update(supplier);

        if (!await repo.SaveAllAsync()) return BadRequest("Couldn't update supplier");

        return NoContent();
    }
}
