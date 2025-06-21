using Application.Abstracts.IServices;
using Application.DTOs.GenralStatistic;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GeneralStatisticsController : Controller
{
    private readonly IGeneralStatisticService _generalStatisticService;

    public GeneralStatisticsController(IGeneralStatisticService generalStatisticService)
    {
        _generalStatisticService = generalStatisticService;
    }

    [HttpGet("get-by-gymId/{gymId:guid}")]
    [Authorize(Roles = IdentityRoleConstants.Admin + "," + IdentityRoleConstants.Owner)]
    public async Task<ActionResult<GeneralStatisticDto>> GetStatisticsByGymAsync(Guid gymId)
    {
        var statistics = await _generalStatisticService.GetStatisticsByGymIdAndPeriodAsync(gymId);
        return Ok(statistics);   
    }
    
    [HttpGet("get-by-ownerId/{ownerId:guid}")]
    [Authorize(Roles = IdentityRoleConstants.Owner)]
    public async Task<ActionResult<GeneralStatisticDto>> GetStatisticsByOwnerAsync(Guid ownerId)
    {
        var statistics = await _generalStatisticService.GetStatisticsByOwnerIdAndPeriodAsync(ownerId);
        return Ok(statistics);   
    }
}