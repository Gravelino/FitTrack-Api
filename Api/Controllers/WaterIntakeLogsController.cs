using Application.Abstracts.IServices;
using Application.DTOs.WaterIntakeLog;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WaterIntakeLogsController: Controller<WaterIntakeLogReadDto, WaterIntakeLogCreateDto,
    WaterIntakeLogUpdateDto, WaterIntakeLog>
{
    private readonly IWaterIntakeLogService _service;

    public WaterIntakeLogsController(IWaterIntakeLogService service) : base(service)
    {
        _service = service;
    }
    
    [HttpGet("get-by-userId-and-day/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<WaterIntakeLogReadDto>>> GetWaterIntakeLogsByUserIdAndDay(Guid userId,
        [FromQuery] DateTime date)
    {
        var waterIntakeLogs = await _service.GetWaterIntakeLogsByUserIdAndDayAsync(userId, date);
        if(!waterIntakeLogs.Any())
            return NotFound();
        
        return Ok(waterIntakeLogs);
    }
}