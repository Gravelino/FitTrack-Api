using AutoMapper;

namespace Application.Mapping;

public class GenericProfile<TReadDto, TCreateDto, TUpdateDto, TEntity> : Profile
{
    protected GenericProfile()
    {
        CreateMap<TEntity, TReadDto>();
        CreateMap<TCreateDto, TEntity>();
        CreateMap<TUpdateDto, TEntity>();
    }
}