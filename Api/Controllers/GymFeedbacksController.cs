using Application.Abstracts.IServices;
using Application.DTOs.GymFeedback;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GymFeedbacksController: Controller<GymFeedbackReadDto, GymFeedbackCreateDto, GymFeedbackUpdateDto, GymFeedback>
{
    private readonly IGymFeedbackService _service;

    public GymFeedbacksController(IGymFeedbackService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-userId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<GymFeedbackReadDto>>> GetByUserIdAsync(Guid userId)
    {
        var feedbacks = await _service.GetFeedbacksByUserIdAsync(userId);
        if(!feedbacks.Any())
            return NotFound();
        
        return Ok(feedbacks);
    }
    
    [HttpGet("get-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<GymFeedbackReadDto>>> GetByGymIdAsync(Guid gymId)
    {
        var feedbacks = await _service.GetFeedbacksByGymIdAsync(gymId);
        if(!feedbacks.Any())
            return NotFound();
        
        return Ok(feedbacks);
    }
    
    [HttpGet("get-by-userId/{userId:guid}/gymId{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<GymFeedbackReadDto>>> GetByUserIdAndGymIdAsync(Guid userId, Guid gymId)
    {
        var feedbacks = await _service.GetFeedbacksByUserIdAndGymIdAsync(userId, gymId);
        if(!feedbacks.Any())
            return NotFound();
        
        return Ok(feedbacks);
    }
}