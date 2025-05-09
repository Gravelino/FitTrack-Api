using Application.DTOs.WaterIntakeLog;
using Domain.Entities;

namespace Application.Mapping;

public class WaterIntakeLogProfile: GenericProfile<WaterIntakeLogReadDto, WaterIntakeLogCreateDto,
    WaterIntakeLogUpdateDto, WaterIntakeLog>
{
    public WaterIntakeLogProfile()
    {
        CreateMap<WaterIntakeLog, WaterIntakeLogReadDto>()
            .ForMember(dest => dest.Date, opt
                => opt.MapFrom(src => src.Date.ToUniversalTime()));
        
        CreateMap<WaterIntakeLogCreateDto, WaterIntakeLog>()
            .ForMember(dest => dest.Date, opt
                => opt.MapFrom(src => src.Date.ToUniversalTime()));
        
        CreateMap<WaterIntakeLogUpdateDto, WaterIntakeLog>()
            .ForMember(dest => dest.Date, opt
                => opt.MapFrom(src => src.Date.ToUniversalTime()));
    }
}