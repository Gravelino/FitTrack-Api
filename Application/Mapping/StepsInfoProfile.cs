using Application.DTOs.StepsInfo;
using Domain.Entities;

namespace Application.Mapping;

public class StepsInfoProfile : GenericProfile<StepsInfoReadDto, StepsInfoCreateDto, StepsInfoUpdateDto, StepsInfo>
{
    public StepsInfoProfile(): base()
    {
        
    }
}