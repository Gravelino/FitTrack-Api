using Application.DTOs.StepsInfo;
using Domain.Entities;

namespace Application.Mapping;

public class StepsInfoProfile : GenericProfile<StepsInfo, StepsInfoReadDto, StepsInfoCreateDto, StepsInfoUpdateDto>
{
    public StepsInfoProfile(): base()
    {
        
    }
}