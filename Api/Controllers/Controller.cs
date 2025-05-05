using Application.Abstracts;
using Application.Abstracts.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public abstract class Controller<TReadDto, TCreateDto, TUpdateDto, TEntity> : ControllerBase
    where TReadDto : class
    where TCreateDto : class
    where TUpdateDto : class, IEntity
    where TEntity : class, IEntity
{
    private readonly IService<TReadDto, TCreateDto, TUpdateDto, TEntity> _service;

    public Controller(IService<TReadDto, TCreateDto, TUpdateDto, TEntity> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TReadDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TReadDto>> GetById(Guid id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity is null)
        {
            return NotFound();
        }
        
        return Ok(entity);
    }

    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(TCreateDto dto)
    {
        var entityId = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = entityId }, entityId);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, TUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }
        
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}