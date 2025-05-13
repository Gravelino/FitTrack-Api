using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using AutoMapper;

namespace Application.Services;

public class Service<TReadDto, TCreateDto, TUpdateDto, TEntity> : IService<TReadDto, TCreateDto, TUpdateDto, TEntity> 
    where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public Service(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<TReadDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TReadDto>>(entities);
    }

    public async Task<TReadDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TReadDto>(entity);
    }

    public virtual async Task<Guid> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        return await _repository.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(Guid id, TUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if(entity is null)
            return;
        
        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(id, entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
       await _repository.DeleteAsync(id);
    }
}