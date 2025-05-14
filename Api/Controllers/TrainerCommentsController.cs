using Application.Abstracts.IServices;
using Application.DTOs.TrainerComment;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TrainerCommentsController: Controller<TrainerCommentReadDto, TrainerCommentCreateDto,
    TrainerCommentUpdateDto, TrainerComment>
{
    private readonly ITrainerCommentService _service;

    public TrainerCommentsController(ITrainerCommentService service) : base(service)
    {
        _service = service;
    }

    [Authorize(Roles = IdentityRoleConstants.Trainer)]
    public override async Task<ActionResult<Guid>> Create(TrainerCommentCreateDto dto) =>
         await base.Create(dto);
    
    [Authorize(Roles = IdentityRoleConstants.Trainer)]
    public override async Task<IActionResult> Update(Guid id, TrainerCommentUpdateDto dto) =>
        await base.Update(id, dto);
    
    [Authorize(Roles = IdentityRoleConstants.Trainer)]
    public override async Task<IActionResult> Delete(Guid id) =>
        await base.Delete(id);
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-by-userId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<TrainerCommentReadDto>>> GetByUserId(Guid userId, [FromQuery] DateTime date)
    {
        var comments = await _service.GetTrainerCommentsByUserIdAndDate(userId, date);
        if(!comments.Any())
            return NotFound();
        
        return Ok(comments);
    }
    
    [Authorize(Roles = IdentityRoleConstants.Trainer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-by-trainerId/{trainerId:guid}")]
    public async Task<ActionResult<IEnumerable<TrainerCommentReadDto>>> GetByTrainerId(Guid trainerId, [FromQuery] DateTime date)
    {
        var comments = await _service.GetTrainerCommentsByTrainerIdAndDate(trainerId, date);
        if(!comments.Any())
            return NotFound();
        
        return Ok(comments);
    }
}