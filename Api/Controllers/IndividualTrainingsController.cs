using Application.Abstracts;
using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.IndividualTraining;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("/api/[controller]")]
[ApiController]
public class IndividualTrainingsController : Controller<IndividualTrainingReadDto, IndividualTrainingCreateDto,
    IndividualTrainingUpdateDto, IndividualTraining>
{
    private new readonly IIndividualTrainingService _service;

    public IndividualTrainingsController(IIndividualTrainingService service ) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-userId-and-period/{userId:guid}/{fromDate:datetime}/{toDate:datetime}")]
    public async Task<ActionResult<IndividualTrainingReadDto>> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId,
        DateTime fromDate, DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return BadRequest();
        }

        var trainings = await _service.GetIndividualTrainingsInfoByUserIdByPeriod(userId,
            fromDate, toDate);
        if (!trainings.Any())
        {
            return NotFound();
        }
        
        return Ok(trainings);
    }
}