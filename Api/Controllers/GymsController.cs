using Application.Abstracts.IServices;
using Application.DTOs.Gym;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GymsController : Controller<GymReadDto, GymCreateDto, GymUpdateDto, Gym>
{
    private readonly IGymService _service;

    public GymsController(IGymService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-ownerId/{ownerId:guid}")]
    public async Task<ActionResult<IEnumerable<GymReadDto>>> GetGymsByOwnerId(Guid ownerId)
    {
        var gyms = await _service.GetGymsByOwnerIdAsync(ownerId);
        if(!gyms.Any())
            return NotFound();
        
        return Ok(gyms);
    }

    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [HttpPost("create-with-images")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<Guid>> Create([FromForm] GymCreateDto dto,
        [FromForm] IFormFile mainImage)
    {
        var gymId = await _service.CreateAsync(dto, mainImage);
        return CreatedAtAction(nameof(GetById), new { id = gymId }, gymId);
    }
    
    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [HttpPut("{id:guid}/update-with-images")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(Guid id, [FromForm] GymUpdateDto dto,
        [FromForm] IFormFile? mainImage)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }
        
        await _service.UpdateAsync(id, dto, mainImage);
        return NoContent();
    }
    
    [Authorize(Roles = IdentityRoleConstants.Owner)]
    public override async Task<IActionResult> Delete(Guid id) =>
        await base.Delete(id);  
    
    [Authorize(Roles = IdentityRoleConstants.Owner + "," + IdentityRoleConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{gymId:guid}/details")]
    public async Task<ActionResult<GymDetailsDto>> GetGymDetails(Guid gymId)
    {
        var gym = await _service.GetGymDetailsAsync(gymId);
        if(gym is null)
            return NotFound();
        
        return Ok(gym);
    }
}