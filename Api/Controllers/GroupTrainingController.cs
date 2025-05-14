using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.GroupTraining;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupTrainingController: Controller<GroupTrainingReadDto, GroupTrainingCreateDto, 
    GroupTrainingUpdateDto, GroupTraining>
{
    private readonly IGroupTrainingService _service;

    public GroupTrainingController(IGroupTrainingService service) : base(service)
    {
        _service = service;
    }

    [Authorize(Roles = IdentityRoleConstants.Trainer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-by-trainerId-and-period/{trainerId:guid}")]
    public async Task<ActionResult<IEnumerable<GroupTrainingReadDto>>> GetGroupTrainingsByTrainerIdAndPeriodAsync(
        Guid trainerId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var groupTrainings = await _service.GetGroupTrainingsByTrainerIdAndPeriodAsync(trainerId, fromDate, toDate);
        if(!groupTrainings.Any())
            return NotFound();
        
        return Ok(groupTrainings);   
    }
    
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-by-gymId-and-period/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<GroupTrainingReadDto>>> GetGroupTrainingsByGymIdAndPeriodAsync(
        Guid gymId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var groupTrainings = await _service.GetGroupTrainingsByGymIdAndPeriodAsync(gymId, fromDate, toDate);
        if(!groupTrainings.Any())
            return NotFound();
        
        return Ok(groupTrainings);  
    }

    [Authorize(Roles = IdentityRoleConstants.Trainer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-users-by-trainingId/{trainingId:guid}")]
    public async Task<ActionResult<IEnumerable<CurrentUserDto>>> GetGroupTrainingUsersByTrainingIdAsync(Guid trainingId)
    {
        var users = await _service.GetGroupTrainingUsersByTrainingIdAsync(trainingId);
        if(!users.Any())
            return NotFound();
        
        return Ok(users); 
    }

    [HttpPost("assign-user/{trainingId:guid}")]
    public async Task<IActionResult> AssignUserToGroupTrainingAsync(Guid trainingId, [FromQuery] Guid userId)
    {
        await _service.AssignUserToTrainingAsync(userId, trainingId);
        return Ok();
    }
}