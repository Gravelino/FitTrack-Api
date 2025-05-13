using Application.Abstracts.IServices;
using Application.DTOs.GymStaff;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdminController: ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("get-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<GymStaffReadDto>>> GetAdminsByGymId(Guid gymId)
    {
        var admins = await _adminService.GetAdminsByGymIdAsync(gymId);
        if(!admins.Any())
            return NotFound();
        
        return Ok(admins);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GymStaffReadDto>> GetAdminById(Guid id)
    {
        var admin = await _adminService.GetAdminByIdAsync(id);
        if(admin is null)
            return NotFound();
        
        return Ok(admin);
    }

    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateAdminAsync([FromBody] GymStaffCreateDto dto)
    {
        if (dto is null)
        {
            return BadRequest("Invalid request");
        }

        try
        {
            var id = await _adminService.CreateAdminAsync(dto);
            return Ok(new {Id = id});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAdmin(Guid id, [FromBody] GymStaffUpdateDto dto)
    {
        if(id != dto.Id)
            return BadRequest();
        
        await _adminService.UpdateAdminAsync(dto);
        return NoContent();
    }
    
    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAdmin(Guid id)
    {
        await _adminService.DeleteAdminByIdAsync(id);
        return NoContent();
    }
    
    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [HttpGet("get-by-ownerId/{ownerId:guid}")]
    public async Task<ActionResult<IEnumerable<GymStaffReadDto>>> GetAdminsByOwnerId(Guid ownerId)
    {
        var admins = await _adminService.GetAdminsByOwnerIdAsync(ownerId);
        if(!admins.Any())
            return NotFound();
        
        return Ok(admins);
    }
}