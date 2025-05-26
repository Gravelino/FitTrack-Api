using Application.Abstracts.IServices;
using Application.DTOs.Gym;
using Application.DTOs.GymStaff;
using Application.DTOs.User;
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
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;

    public UserController(UserManager<User> userManager, IUserService userService)
    {
        _userManager = userManager;
        _userService = userService;
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = IdentityRoleConstants.User)]
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
    [Authorize(Roles = IdentityRoleConstants.User)]
    [HttpGet("get-gym-by-userId/{userId:guid}")]
    public async Task<ActionResult<GymReadDto>> GetGymByUserId(Guid userId)
    {
        var gym = await _userService.GetGymByUserId(userId);
        if(gym is null)
            return NotFound();
        
        return Ok(gym);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = IdentityRoleConstants.User)]
    [HttpGet("get-trainer-by-userId/{userId:guid}")]
    public async Task<ActionResult<GymStaffReadDto>> GetTrainerByUserId(Guid userId)
    {
        var trainer = await _userService.GetTrainerByUserId(userId);
        if(trainer is null)
            return NotFound();
        
        return Ok(trainer);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = IdentityRoleConstants.User)]
    [HttpGet("get-active-membership-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<UserMembershipReadDto>> GetActiveMembershipByGymId([FromQuery] Guid userId,
        Guid gymId)
    {
        var membership = await _userService.GetActiveMembershipByGymId(userId, gymId);
        if(membership is null)
            return NotFound();
        
        return Ok(membership);
    }

    [Authorize(Roles = IdentityRoleConstants.User + "," + IdentityRoleConstants.Trainer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("details/{userId:guid}")]
    public async Task<ActionResult<UserDetailsDto>> GetUserDetails(Guid userId, [FromQuery] Guid gymId)
    {
        var details = await _userService.GetUserDetailsAsync(userId, gymId);
        if(details is null)
            return NotFound();
        
        return Ok(details);
    }
}