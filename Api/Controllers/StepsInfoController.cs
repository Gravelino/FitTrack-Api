using Application.Abstracts.IServices;
using Application.DTOs.StepsInfo;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class StepsInfoController : Controller<StepsInfoReadDto, StepsInfoCreateDto, StepsInfoUpdateDto, StepsInfo>
{
    private readonly IStepsInfoService _service;

    public StepsInfoController(IStepsInfoService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-userId-and-period/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<StepsInfoReadDto>>> GetByUserIdAndPeriod(Guid userId,
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        var steps = await _service.GetStepsInfoByUserIdAndPeriod(userId, fromDate, toDate);
        if(!steps.Any())
            return NotFound();
        
        return Ok(steps);
    }
}