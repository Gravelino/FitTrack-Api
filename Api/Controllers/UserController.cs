using Application.Abstracts;
using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.Gym;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IGymService _gymService;

    public UserController(UserManager<User> userManager, IGymService gymService)
    {
        _userManager = userManager;
        _gymService = gymService;
    }
    
    [Authorize(Roles = IdentityRoleConstants.User)]
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
}