using Application.Abstracts.IServices;
using Application.DTOs.Gym;
using Application.DTOs.UserMembership;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = IdentityRoleConstants.User)]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IGymService _gymService;
    private readonly ITrainerService _trainerService;
    private readonly IUserMembershipService _userMembershipService;

    public UserController(UserManager<User> userManager, IGymService gymService, ITrainerService trainerService, IUserMembershipService userMembershipService)
    {
        _userManager = userManager;
        _gymService = gymService;
        _trainerService = trainerService;
        _userMembershipService = userMembershipService;
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("patch-user-details/{userId:guid}")]
    public async Task<IActionResult> PatchUserDetails(Guid userId , [FromBody] JsonPatchDocument<User> userPatch)
    {
        if (userPatch is null)
        {
            return BadRequest();
        }
        
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return NotFound();
        }
        
        userPatch.ApplyTo(user);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-gym-by-userId/{userId:guid}")]
    public async Task<ActionResult<GymReadDto>> GetGymByUserId(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return NotFound();
        
        var gymId = user.GymId;
        if (gymId is null)
            return NotFound();
        
        var gym = await _gymService.GetByIdAsync((Guid)gymId);
        if(gym is null)
            return NotFound();
        
        return Ok(gym);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-trainer-by-userId/{userId:guid}")]
    public async Task<ActionResult<GymReadDto>> GetTrainerByUserId(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return NotFound();
        
        var trainerId = user.TrainerId;
        if (trainerId is null)
            return NotFound();
        
        var trainer = await _trainerService.GetTrainerByIdAsync((Guid)trainerId);
        if(trainer is null)
            return NotFound();
        
        return Ok(trainer);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-active-membership-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<UserMembershipReadDto>> GetActiveMembershipByGymId([FromQuery] Guid userId,
        Guid gymId)
    {
        var membership = await _userMembershipService.GetUserActiveMembershipByUserIdAndGymIdAsync(userId, gymId);
        if(membership is null)
            return NotFound();
        
        return Ok(membership);
    }
}