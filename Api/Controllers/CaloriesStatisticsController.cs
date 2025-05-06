using Application.Abstracts.IServices;
using Application.DTOs.Meal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CaloriesStatisticsController : ControllerBase
{
    private readonly ICalorieStatisticsService _calorieStatisticsService;

    public CaloriesStatisticsController(ICalorieStatisticsService calorieStatisticsService)
    {
        _calorieStatisticsService = calorieStatisticsService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<IEnumerable<CaloriesStatisticsDto>>> GetCaloriesStatistics(
        Guid userId,
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate,
        [FromQuery] string caloriesGroupBy)
    {
        if (!Enum.TryParse<CaloriesGroupBy>(caloriesGroupBy, true, out var groupBy))
        {
            return BadRequest("Invalid goal type");
        }
        
        var result = await _calorieStatisticsService.GetStatisticsAsync(userId, fromDate, toDate, groupBy);
        return Ok(result);
    }
}