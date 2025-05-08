using Application.DTOs.Sleep;
using Domain.Entities;

namespace Application.Mapping;

public class SleepProfile : GenericProfile<SleepReadDto, SleepCreateDto, SleepUpdateDto, Sleep>
{
    public SleepProfile()
    {
        CreateMap<Sleep, SleepReadDto>()
            .ForMember(dest => dest.SleepStart, opt
                => opt.MapFrom(src => src.SleepStart.ToUniversalTime()))
            .ForMember(dest => dest.WakeUpTime, opt
                => opt.MapFrom(src => src.WakeUpTime.ToUniversalTime()));
        
        CreateMap<SleepCreateDto, Sleep>()
            .ForMember(dest => dest.SleepStart, opt
                => opt.MapFrom(src => src.SleepStart.ToUniversalTime()))
            .ForMember(dest => dest.WakeUpTime, opt
                => opt.MapFrom(src => src.WakeUpTime.ToUniversalTime()));
        
        CreateMap<SleepUpdateDto, Sleep>()
            .ForMember(dest => dest.SleepStart, opt
                => opt.MapFrom(src => src.SleepStart.ToUniversalTime()))
            .ForMember(dest => dest.WakeUpTime, opt
                => opt.MapFrom(src => src.WakeUpTime.ToUniversalTime()));
    }
}