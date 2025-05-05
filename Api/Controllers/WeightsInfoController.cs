using Application.Abstracts.IServices;
using Application.DTOs.WeightsInfo;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WeightsInfoController : Controller<WeightsInfoReadDto, WeightsInfoCreateDto, WeightsInfoUpdateDto, WeightsInfo>
{
    private readonly IWeightsInfoService _service;

    public WeightsInfoController(IWeightsInfoService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-userId-and-period/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<WeightsInfoReadDto>>> GetByUserIdAndPeriod(Guid userId,
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        var weights = await _service.GetWeightsInfoByUserIdAndPeriod(userId, fromDate, toDate);
        if (!weights.Any())
            return NotFound();
        
        return Ok(weights);
    }
}