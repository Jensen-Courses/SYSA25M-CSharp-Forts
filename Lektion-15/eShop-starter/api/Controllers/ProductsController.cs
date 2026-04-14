using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController() : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> ListAllProducts(string? brand, string? sort)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindProduct(int id)
        {
            return Ok();
        }

        [HttpGet("product/{itemNumber}")]
        public async Task<ActionResult> FindProduct(string itemNumber)
        {
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> AddProduct()
        {
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            return NoContent();
        }
    }
}
