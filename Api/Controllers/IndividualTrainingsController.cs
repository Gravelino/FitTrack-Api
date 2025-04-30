using Application.Abstracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("/api/[controller]")]
[ApiController]
public class IndividualTrainingsController : Controller<IndividualTraining>
{
    private new readonly IIndividualTrainingRepository _repository;

    public IndividualTrainingsController(IIndividualTrainingRepository repository ) : base(repository)
    {
        _repository = repository;
    }

    [HttpGet("get-by-userId-and-period/{userId:guid}/{fromDate:datetime}/{toDate:datetime}")]
    public async Task<IActionResult> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId, DateTime fromDate,
        DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return BadRequest();
        }

        var trainings = await _repository.GetIndividualTrainingsInfoByUserIdByPeriod(userId,
            fromDate, toDate);
        if (!trainings.Any())
        {
            return NotFound();
        }
        
        return Ok(trainings);
    }
}