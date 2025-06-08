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
    public async Task<ActionResult<IEnumerable<PurchaseStatisticsGroupedDto>>> GetStatisticsByGymAsync(Guid gymId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string purchasesGroupBy)
    {
        if (!Enum.TryParse<PurchasesGroupBy>(purchasesGroupBy, true, out var groupBy))
        {
            return BadRequest("Invalid goal type");
        }
        if (fromDate > toDate)
        {
            return BadRequest("Invalid date");
        }
        
        var statistics =
            await _purchaseStatisticsService.GetStatisticsByGymIdAndPeriodAsync(gymId, fromDate, toDate, groupBy);
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
        
        var statistics = await _purchaseStatisticsService.GetStatisticsByOwnerIdAndPeriodAsync(ownerId, fromDate, toDate);
        return Ok(statistics);   
    }
}