using api.DTOs.Products;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class ProductsController(
        IUnitOfWork uow, IMapper mapper) : ApiBaseController
{

    [HttpGet()]
    public async Task<ActionResult> ListAllProducts([FromQuery] ProductSpecificationParams args)
    {
        var spec = new ProductSpecification(args);
        var result = await uow.Repository<Product>().ListAsync(spec);
        var products = mapper.Map<IReadOnlyList<ListProductsDto>>(result);
        return await CreatePagedResult(uow.Repository<Product>(), spec, args.PageNumber, args.PageSize, products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(string id)
    {
        var spec = new ProductSpecification(id);
        var result = await uow.Repository<Product>().FindAsync(spec);
        var product = mapper.Map<ProductDto>(result);
        return Ok(product);
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto model)
    {
        try
        {
            var product = mapper.Map<Product>(model);

            // Hämta ett leverantörs objekt baserat på modellens supplier egenskap.
            // SupplierSpecificationParams...
            var supplierArgs = new SupplierSpecificationParams
            {
                SupplierName = model.SupplierName
            };
            // SupplierSpecification...
            var supplierSpec = new SupplierSpecification(supplierArgs);

            // Hämta leverantörer ifrån databasen...
            var supplier = await uow.Repository<Supplier>().FindAsync(supplierSpec);

            if (supplier is null) return BadRequest($"Ingen leverantör hittades med namnet {model.SupplierName}");

            product.Supplier = supplier;

            uow.Repository<Product>().Add(product);

            if (await uow.Complete())
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

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(string id, PutProductDto productDto)
    {
        if (id != productDto.Id || !ProductExists(id)) return BadRequest("Cannot update this product");

        var product = mapper.Map<Product>(productDto);

        var supplier = await FindSupplier(productDto.SupplierName);
        if (supplier is null) return BadRequest("Leverantör existerar inte!");

        product.Supplier = supplier;
        uow.Repository<Product>().Update(product);

        if (await uow.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(string id)
    {
        try
        {
            var product = await uow.Repository<Product>().FindByIdAsync(id);
            if (product is null) return BadRequest("Hittade ingen product");

            uow.Repository<Product>().Delete(product);

            if (await uow.Complete()) return NoContent();

            return StatusCode(500, "Ett server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Ett server fel inträffade");
        }
    }

    private async Task<Supplier?> FindSupplier(string name)
    {
        var supplierArgs = new SupplierSpecificationParams
        {
            SupplierName = name
        };

        var supplierSpec = new SupplierSpecification(supplierArgs);
        var supplier = await uow.Repository<Supplier>().FindAsync(supplierSpec);

        return supplier;
    }

    private bool ProductExists(string id)
    {
        return uow.Repository<Product>().CheckIfExists(id);
    }
}

