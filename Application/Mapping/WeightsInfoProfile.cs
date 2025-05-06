using Application.DTOs.WeightsInfo;
using Domain.Entities;

namespace Application.Mapping;

public class WeightsInfoProfile : GenericProfile<WeightsInfo, WeightsInfoReadDto, WeightsInfoCreateDto,
    WeightsInfoUpdateDto>
{
    public WeightsInfoProfile(): base()
    {
        
    }
}