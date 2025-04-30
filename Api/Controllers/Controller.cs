using Application.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public abstract class Controller<T> : ControllerBase where T : class, IEntity
{
    protected readonly IRepository<T> _repository;

    public Controller(IRepository<T> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetAll()
    {
        var result = await _repository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<T>> GetById(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return NotFound();
        }
        
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<T>> Create(T entity)
    {
        var entityId = await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entityId }, entityId);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<T>> Update(Guid id, T entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        await _repository.UpdateAsync(id, entity);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}