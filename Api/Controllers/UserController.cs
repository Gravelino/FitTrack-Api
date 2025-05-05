using Application.Abstracts;
using Application.Abstracts.IServices;
using Application.DTOs;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IOwnerService _ownerService;
    private readonly IAdminService _adminService;

    public UserController(IOwnerService ownerService, IAdminService adminService)
    {
        _ownerService = ownerService;
        _adminService = adminService;
    }

    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [HttpPost("create-trainer")]
    public async Task<IActionResult> CreateTrainerAsync([FromBody] CreateStaffDto dto)
    {
        if (dto is null)
        {
            return BadRequest("Invalid request");
        }

        try
        {
            await _adminService.CreateTrainerAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdminAsync([FromBody] CreateStaffDto dto)
    {
        if (dto is null)
        {
            return BadRequest("Invalid request");
        }

        try
        {
            await _ownerService.CreateAdminAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}