using Application.DTOs.WeightsInfo;
using Domain.Entities;

namespace Application.Mapping;

public class WeightsInfoProfile : GenericProfile<WeightsInfoCreateDto, WeightsInfoReadDto, WeightsInfoUpdateDto,
    WeightsInfo>
{
    public WeightsInfoProfile(): base()
    {
        
    }
}