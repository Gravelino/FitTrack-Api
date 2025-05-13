using Application.Abstracts.IServices;
using Application.DTOs.Product;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController: Controller<ProductReadDto, ProductCreateDto, ProductUpdateDto, Product>
{
    private readonly IProductService _service;

    public ProductsController(IProductService service) : base(service)
    {
        _service = service;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-by-type/{type}")]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetByType(string type)
    {
        var products = await _service.GetAllProductsAsync(type);
        if(!products.Any())
            return NotFound();
        
        return Ok(products);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-by-gymId/{type}")]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetByGymIdAndType(Guid gymId, string type)
    {
        var products = await _service.GetProductsByGymId(gymId, type);
        if(!products.Any())
            return NotFound();
        
        return Ok(products);
    }
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [HttpPost("add-service")]
    public override async Task<ActionResult<Guid>> Create(ProductCreateDto dto)
        => await base.Create(dto);
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [HttpPut("update-service/{id:guid}")]
    public override async Task<IActionResult> Update(Guid id, ProductUpdateDto dto)
        => await base.Update(id, dto);
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    public override async Task<IActionResult> Delete(Guid id)
        => await base.Delete(id);

    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("add-good")]
    public async Task<ActionResult<Guid>> AddGood([FromForm ]ProductCreateDto dto, [FromForm] IFormFile file)
    {
        var id = await _service.CreateGoodAsync(dto, file);
        return CreatedAtAction(nameof(GetById), new { Id = id }, id);
    }
    
    [Authorize(Roles = IdentityRoleConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("update-good/{id:guid}")]
    public async Task<IActionResult> UpdateGood(Guid id, [FromForm] ProductUpdateDto dto, [FromForm] IFormFile file)
    {
        if(id != dto.Id)
            return BadRequest();
        
        await _service.UpdateGoodAsync(id, dto, file);
        return NoContent();
    }
}