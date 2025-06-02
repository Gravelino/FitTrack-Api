using Application.Abstracts.IServices;
using Application.DTOs.Purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PurchaseStatisticsController: ControllerBase
{
    private readonly IPurchaseStatisticsService _purchaseStatisticsService;

    public PurchaseStatisticsController(IPurchaseStatisticsService purchaseStatisticsService)
    {
        _purchaseStatisticsService = purchaseStatisticsService;
    }

    [HttpGet("get-by-gymId/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<PurchaseStatisticsDto>>> GetStatisticsByGymAsync(Guid gymId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return BadRequest("Invalid date");
        }
        
        var statistics = await _purchaseStatisticsService.GetStatisticsByGymIdAndPeriodAsync(gymId, fromDate, toDate);
        return Ok(statistics);   
    }
    
    [HttpGet("get-by-ownerId/{ownerId:guid}")]
    public async Task<ActionResult<IEnumerable<PurchaseStatisticsDto>>> GetStatisticsByOwnerAsync(Guid ownerId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return BadRequest("Invalid date");
        }
        
        var statistics = await _purchaseStatisticsService.GetStatisticsByGymIdAndPeriodAsync(ownerId, fromDate, toDate);
        return Ok(statistics);   
    }
}