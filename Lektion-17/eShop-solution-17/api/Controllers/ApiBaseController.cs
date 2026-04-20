using api.Helpers;
using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiBaseController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
        ISpecification<T> args, int pageNumber, int pageSize) where T : BaseEntity
    {
        var result = await repo.ListAsync(args);
        var items = await repo.CountAsync(args);
        var response = new PagedResult<T>(pageNumber, pageSize, items, result);

        return Ok(response);
    }
}
