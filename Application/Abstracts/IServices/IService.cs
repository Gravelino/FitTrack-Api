namespace Application.Abstracts.IServices;

public interface IService<TReadDto, TCreateDto, TUpdateDto, TEntity> where TEntity : class, IEntity
{
    Task<IEnumerable<TReadDto>> GetAllAsync();
    Task<TReadDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(TCreateDto dto);
    Task UpdateAsync(Guid id, TUpdateDto dto);
    Task DeleteAsync(Guid id);
}