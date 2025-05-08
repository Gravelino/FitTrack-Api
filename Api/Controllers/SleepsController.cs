using Application.Abstracts.IServices;
using Application.DTOs.Sleep;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SleepsController : Controller<SleepReadDto, SleepCreateDto, SleepUpdateDto, Sleep>
{
    private readonly ISleepService _service;

    public SleepsController(ISleepService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-userId-and-day/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<SleepReadDto>>> GetSleepsByUserIdAndDay(Guid userId,[FromQuery] DateTime date)
    {
        var sleeps = await _service.GetSleepsByUserIdAndDayAsync(userId, date);
        if(!sleeps.Any())
            return NotFound();
        
        return Ok(sleeps);
    }
}