using Application.Abstracts.IServices;
using Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserStatisticController : Controller
{
    private readonly IUserStatisticService _service;

    public UserStatisticController(IUserStatisticService service)
    {
        _service = service;
    }
    
    [HttpGet("get-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<UserStatisticGroupedDto>>> GetStatisticsByGymAsync(Guid gymId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string usersGroupBy)
    {
        if (!Enum.TryParse<UsersGroupBy>(usersGroupBy, true, out var groupBy))
        {
            return BadRequest("Invalid goal type");
        }
        if (fromDate > toDate)
        {
            return BadRequest("Invalid date");
        }
        
        var statistics =
            await _service.GetStatisticsByGymIdAndPeriodAsync(gymId, fromDate, toDate, groupBy);
        return Ok(statistics);   
    }
    
    [HttpGet("get-by-ownerId/{ownerId:guid}")]
    public async Task<ActionResult<IEnumerable<UserStatisticDto>>> GetStatisticsByOwnerAsync(Guid ownerId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return BadRequest("Invalid date");
        }
        
        var statistics = await _service.GetStatisticsByOwnerIdAndPeriodAsync(ownerId, fromDate, toDate);
        return Ok(statistics);   
    }
}