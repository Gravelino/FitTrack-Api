using Application.Abstracts.IServices;
using Application.DTOs.Gym;
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
}