using Application.DTOs.StepsInfo;
using Domain.Entities;

namespace Application.Mapping;

public class StepsInfoProfile : GenericProfile<StepsInfoCreateDto, StepsInfoReadDto, StepsInfoUpdateDto, StepsInfo>
{
    public StepsInfoProfile(): base()
    {
        
    }
}