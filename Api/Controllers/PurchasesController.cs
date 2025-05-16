using Application.Abstracts.IServices;
using Application.DTOs.Purchase;
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
}