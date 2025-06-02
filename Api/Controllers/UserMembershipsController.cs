using Application.Abstracts.IServices;
using Application.DTOs.UserMembership;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserMembershipsController: Controller<UserMembershipReadDto, UserMembershipCreateDto,
    UserMembershipUpdateDto, UserMembership>
{
    private readonly IUserMembershipService _service;

    public UserMembershipsController(IUserMembershipService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-history-by-userId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<UserMembershipReadDto>>> GetUserMembershipsHistoryByUserIdAsync(Guid userId)
    {
        var userMemberships = await _service.GetUserMembershipsHistoryByUserIdAsync(userId);
        if(!userMemberships.Any())
            return NotFound();
        
        return Ok(userMemberships);
    }

    [HttpGet("get-active-memberships-by-userId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<UserMembershipReadDto>>> GetUserActiveMembershipsByUserIdAsync(Guid userId)
    {
        var userMemberships = await _service.GetUserActiveMembershipsByUserIdAsync(userId);
        if(!userMemberships.Any())
            return NotFound();
        
        return Ok(userMemberships);
    }

    [HttpGet("get-pending-memberships-by-userId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<UserMembershipReadDto>>> GetUserPendingMembershipsByUserIdAsync(Guid userId)
    {
        var userMemberships = await _service.GetUserPendingMembershipsByUserIdAsync(userId);
        if(!userMemberships.Any())
            return NotFound();
        
        return Ok(userMemberships);
    }

    public override async Task<ActionResult<Guid>> Create(UserMembershipCreateDto dto)
    {
        var id = await _service.CreateUserMembershipAsync(dto);
        return CreatedAtAction(nameof(GetById), new {id}, id);   
    }   
    
    [Authorize(Roles = IdentityRoleConstants.Admin + "," + IdentityRoleConstants.Owner)]
    [HttpGet("get-history-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<UserMembershipReadDto>>> GetUserMembershipsHistoryByGymIdAsync(Guid gymId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var userMemberships = await _service.GetUserMembershipsHistoryByGymIdAsync(gymId,
            fromDate, toDate);
        if(!userMemberships.Any())
            return NotFound();
        
        return Ok(userMemberships);
    }
    
    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [HttpGet("get-history-by-ownerId/{owner:guid}")]
    public async Task<ActionResult<IEnumerable<UserMembershipReadDto>>> GetUserMembershipsHistoryByOwnerIdAsync(
        Guid ownerId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var userMemberships =
            await _service.GetUserMembershipsHistoryByOwnerIdAndPeriodAsync(ownerId, fromDate, toDate);
        if(!userMemberships.Any())
            return NotFound();
        
        return Ok(userMemberships);
    }
}