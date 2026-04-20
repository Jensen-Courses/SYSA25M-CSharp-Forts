using api.DTOs.Products;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ProductsController(
            IGenericRepository<Product> repo,
            IGenericRepository<Supplier> supplierRepo,
            IMapper mapper) : ApiBaseController
    {
        [HttpGet()]
        public async Task<ActionResult> ListAllProducts([FromQuery] ProductSpecificationParams args)
        {
            var spec = new ProductSpecification(args);
            return await CreatePagedResult(repo, spec, args.PageNumber, args.PageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindProduct(string id)
        {
            var product = await repo.FindByIdAsync(id);
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
                var supplier = await supplierRepo.FindAsync(supplierSpec);

                if (supplier is null) return BadRequest($"Ingen leverantör hittades med namnet {model.Brand}");

                product.Supplier = supplier;

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
}
