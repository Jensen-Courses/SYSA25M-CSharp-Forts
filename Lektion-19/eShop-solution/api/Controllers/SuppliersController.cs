using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class SuppliersController(IUnitOfWork uow, IMapper mapper) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> ListAllSuppliers([FromQuery] SupplierSpecificationParams args)
    {
        var spec = new SupplierSpecification(args);
        var result = await uow.Repository<Supplier>().ListAsync(spec);
        return await CreatePagedResult(uow.Repository<Supplier>(), spec, args.PageNumber, args.PageSize, result);
    }

    [HttpPost]
    public async Task<ActionResult> AddSupplier(PostSupplierDto model)
    {
        var supplier = mapper.Map<Supplier>(model);
        uow.Repository<Supplier>().Add(supplier);

        if (await uow.Complete())
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
        uow.Repository<Supplier>().Update(supplier);

        if (!await uow.Complete()) return BadRequest("Couldn't update supplier");

        return NoContent();
    }
}
