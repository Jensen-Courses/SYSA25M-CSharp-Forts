using api.DTOs.Orders;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class AdminController(IUnitOfWork uow, IMapper mapper) : ApiBaseController
{
    [HttpPost("delivery")]
    public async Task<ActionResult> AddDeliveryMethod(PostDeliveryMethodDto deliveryDto)
    {
        var deliveryMethod = mapper.Map<DeliveryMethod>(deliveryDto);
        uow.Repository<DeliveryMethod>().Add(deliveryMethod);

        if (await uow.Complete()) return StatusCode(201, deliveryMethod);

        return BadRequest();
    }
}
