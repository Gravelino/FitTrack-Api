using Application.DTOs.WeightsInfo;
using Domain.Entities;

namespace Application.Mapping;

public class WeightsInfoProfile : GenericProfile<WeightsInfoReadDto, WeightsInfoCreateDto, WeightsInfoUpdateDto,
    WeightsInfo>
{
    public WeightsInfoProfile(): base()
    {
        
    }
}