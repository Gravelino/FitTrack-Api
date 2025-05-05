using Application.DTOs.Set;
using Domain.Entities;

namespace Application.Mapping;

public class SetProfile : GenericProfile<Set, SetReadDto, SetCreateDto, SetUpdateDto>
{
    public SetProfile() : base()
    {
        
    }
}