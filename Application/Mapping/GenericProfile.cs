using AutoMapper;

namespace Application.Mapping;

public class GenericProfile<TEntity, TReadDto, TCreateDto, TUpdateDto> : Profile
{
    protected GenericProfile()
    {
        CreateMap<TEntity, TReadDto>();
        CreateMap<TCreateDto, TEntity>();
        CreateMap<TUpdateDto, TEntity>();
    }
}