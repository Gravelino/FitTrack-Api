using Application.Abstracts.IServices;
using Application.DTOs.GymStaff;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TrainerController: ControllerBase
{
    private readonly ITrainerService _trainerService;

    public TrainerController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }
    
    [HttpGet("get-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<GymStaffReadDto>>> GetTrainersByGymId(Guid gymId)
    {
        var trainers = await _trainerService.GetTrainersByGymIdAsync(gymId);
        if(!trainers.Any())
            return NotFound();
        
        return Ok(trainers);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GymStaffReadDto>> GetTrainerById(Guid id)
    {
        var trainer = await _trainerService.GetTrainerByIdAsync(id);
        if(trainer is null)
            return NotFound();
        
        return Ok(trainer);
    }
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateTrainerAsync([FromForm] GymStaffCreateDto dto, [FromForm] IFormFile profileImage)
    {
        if (dto is null)
        {
            return BadRequest("Invalid request");
        }

        try
        {
            await _trainerService.CreateTrainerAsync(dto, profileImage);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTrainer(Guid id, [FromBody] GymStaffUpdateDto dto)
    {
        if(id != dto.Id)
            return BadRequest();
        
        await _trainerService.UpdateTrainerAsync(dto);
        return NoContent();
    }
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("update-with-image/{id:guid}")]
    public async Task<IActionResult> UpdateTrainer(Guid id, [FromForm] GymStaffUpdateDto dto,
        [FromForm] IFormFile profileImage)
    {
        if(id != dto.Id)
            return BadRequest();
        
        await _trainerService.UpdateTrainerWithImageAsync(dto, profileImage);
        return NoContent();
    }
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrainer(Guid id)
    {
        await _trainerService.DeleteTrainerByIdAsync(id);
        return NoContent();
    }
}