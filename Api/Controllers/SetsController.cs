using Application.Abstracts;
using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.Set;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("/api/[controller]")]
[ApiController]
public class SetsController : Controller<SetReadDto, SetCreateDto, SetUpdateDto, Set>
{
    private readonly ISetService _service;

    public SetsController(ISetService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-individual-training-Id/{individualTrainingId:guid}")]
    public async Task<ActionResult<IEnumerable<SetReadDto>>> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId)
    {
        var sets = await _service.GetSetsInfoByIndividualTrainingId(individualTrainingId);
        if(!sets.Any())
            return NotFound();
        
        return Ok(sets);
    }
}