using api.DTOs.Products;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ProductsController(
            IUnitOfWork uow, IMapper mapper) : ApiBaseController
    {
        [HttpGet()]
        public async Task<ActionResult> ListAllProducts([FromQuery] ProductSpecificationParams args)
        {
            var spec = new ProductSpecification(args);
            return await CreatePagedResult(uow.Repository<Product>(), spec, args.PageNumber, args.PageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindProduct(string id)
        {
            var product = await uow.Repository<Product>().FindByIdAsync(id);
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
                    SupplierName = model.Brand
                };
                // SupplierSpecification...
                var supplierSpec = new SupplierSpecification(supplierArgs);

                // Hämta leverantörer ifrån databasen...
                var supplier = await uow.Repository<Supplier>().FindAsync(supplierSpec);

                if (supplier is null) return BadRequest($"Ingen leverantör hittades med namnet {model.Brand}");

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
        public async Task<ActionResult> UpdateProduct(int id)
        {
            return NoContent();
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
    }
}
