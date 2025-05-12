using Application.Abstracts.IServices;
using Application.DTOs.Membership;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MembershipsController: Controller<MembershipReadDto, MembershipCreateDto, MembershipUpdateDto, Membership>
{
    private readonly IMembershipService _service;

    public MembershipsController(IMembershipService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<MembershipReadDto>>> GetByGymId(Guid gymId)
    {
        var memberships = await _service.GetMembershipByGymIdAsync(gymId);
        if(!memberships.Any())
            return NotFound();
        
        return Ok(memberships);
    }
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    public override async Task<ActionResult<Guid>> Create(MembershipCreateDto dto) =>
        await base.Create(dto);   

    [Authorize(Roles = IdentityRoleConstants.Admin)]
    public override async Task<IActionResult> Update(Guid id, MembershipUpdateDto dto) =>
        await base.Update(id, dto);   
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    public override async Task<IActionResult> Delete(Guid id) =>
        await base.Delete(id);   
}