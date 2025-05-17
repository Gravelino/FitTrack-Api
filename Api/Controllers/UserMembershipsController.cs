using Application.Abstracts.IServices;
using Application.DTOs.UserMembership;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
}