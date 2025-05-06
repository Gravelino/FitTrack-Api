using Application.Abstracts.IServices;
using Application.DTOs.UserGoal;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserGoalController : Controller<UserGoalReadDto, UserGoalCreateDto, UserGoalUpdateDto, UserGoal>
{
    private readonly IUserGoalService _service;

    public UserGoalController(IUserGoalService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-userId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<UserGoalReadDto>>> GetUserGoalsByUserIdAsync(Guid userId)
    {
        var userGoals = await _service.GetUserGoalsByUserIdAsync(userId);
        if(!userGoals.Any())
            return NotFound();
        
        return Ok(userGoals);
    }
    
    [HttpGet("get-by-userId-and-goal-type/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<UserGoalReadDto>>> GetUserGoalsByUserIdAsync(Guid userId,
        [FromQuery] string goalType)
    {
        if (!Enum.TryParse<Goal>(goalType, true, out var type))
        {
            return BadRequest("Invalid goal type");
        }
        
        var userGoals = await _service.GetAllUserGoalsByUserIdAndGoalTypeAsync(userId, type);
        if(!userGoals.Any())
            return NotFound();
        
        return Ok(userGoals);
    }
}