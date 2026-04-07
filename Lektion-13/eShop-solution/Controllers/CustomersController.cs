using eShop.DTOs.Customers;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomersController(IGenericRepository<Customer> repo) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllCustomers()
    {
        var customers = await repo.ListAllAsync();
        return Ok(new { Success = true, StatusCode = 200, Items = customers.Count, Data = customers });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindCustomer(int id)
    {
        var customer = await repo.FindByIdAsync(id);
        return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = customer });
    }

    [HttpPost()]
    public async Task<ActionResult> AddCustomer(Customer customer)
    {
        try
        {
            repo.Add(customer);
            if (await repo.SaveAllAsync()) return StatusCode(201);

            return StatusCode(500, "Ett server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Ett server fel inträffade");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
    {
        try
        {
            customer.Id = id;
            repo.Update(customer);

            if (await repo.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Något server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade");
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        try
        {
            var customer = await repo.FindByIdAsync(id);
            if (customer is null) return NotFound("Hittade inte kunden");

            repo.Delete(customer);

            if (await repo.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Ett server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Ett server fel inträffade");
        }
    }
}

