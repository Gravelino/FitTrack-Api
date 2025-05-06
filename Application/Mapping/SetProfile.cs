using Application.DTOs.Set;
using Domain.Entities;

namespace Application.Mapping;

public class SetProfile : GenericProfile<SetReadDto, SetCreateDto, SetUpdateDto, Set>
{
    public SetProfile() : base()
    {
        
    }
}