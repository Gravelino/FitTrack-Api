using Application.Abstracts.IServices;
using Application.DTOs.Sleep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class SleepStatisticsController : ControllerBase
{
    private readonly ISleepStatisticService _sleepStatisticService;

    public SleepStatisticsController(ISleepStatisticService sleepStatisticService)
    {
        _sleepStatisticService = sleepStatisticService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<IEnumerable<SleepStatisticsDto>>> GetCaloriesStatistics(
        Guid userId,
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return BadRequest("Invalid date");
        }
        
        var result = await _sleepStatisticService.GetStatisticsAsync(userId, fromDate, toDate);
        return Ok(result);
    }
}