using Application.Abstracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("/api/[controller]")]
[ApiController]
public class SetsController : Controller<Set>
{
    private new readonly ISetRepository _repository;

    public SetsController(ISetRepository repository) : base(repository)
    {
        _repository = repository;
    }

    [HttpGet("get-by-individual-training-Id/{individualTrainingId:guid}")]
    public async Task<IActionResult> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId)
    {
        var sets = await _repository.GetSetsInfoByIndividualTrainingId(individualTrainingId);
        if(!sets.Any())
            return NotFound();
        
        return Ok(sets);
    }
}