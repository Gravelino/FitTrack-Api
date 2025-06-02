using Application.Abstracts.IServices;
using Application.DTOs.Purchase;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PurchasesController: Controller<PurchaseReadDto, PurchaseCreateDto, PurchaseUpdateDto, Purchase>
{
    private readonly IPurchaseService _service;

    public PurchasesController(IPurchaseService service) : base(service)
    {
        _service = service;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-history-by-userId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<PurchaseReadDto>>> GetPurchasesHistoryByUserId(Guid userId)
    {
        var purchases = await _service.GetPurchasesHistoryByUserIdAsync(userId);
        if(!purchases.Any())
            return NotFound();
        
        return Ok(purchases);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = IdentityRoleConstants.Admin + "," + IdentityRoleConstants.Owner)]
    [HttpGet("get-history-by-gymId-and-period/{gymId:guid}")]
    public async Task<ActionResult<IEnumerable<PurchaseReadDto>>> GetPurchasesHistoryByGymIdAndPeriod(Guid gymId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var purchases = await _service.GetPurchasesHistoryByGymIdAndPeriodAsync(gymId, fromDate, toDate);
        if(!purchases.Any())
            return NotFound();
        
        return Ok(purchases);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = IdentityRoleConstants.Owner)]
    [HttpGet("get-history-by-ownerId-and-period/{ownerId:guid}")]
    public async Task<ActionResult<IEnumerable<PurchaseReadDto>>> GetPurchasesHistoryByOwnerIdAndPeriod(Guid ownerId,
        [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var purchases = await _service.GetPurchasesHistoryByOwnerIdAndPeriodAsync(ownerId,
            fromDate, toDate);
        if(!purchases.Any())
            return NotFound();
        
        return Ok(purchases);
    }
}