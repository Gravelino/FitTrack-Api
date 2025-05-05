using Application.DTOs.UserGoal;
using Domain.Entities;
using AutoMapper;
using Domain.Enums;

namespace Application.Mapping;

public class UserGoalProfile : GenericProfile<UserGoalCreateDto, UserGoalReadDto, UserGoalUpdateDto, UserGoal>
{
    public UserGoalProfile()
    {
        CreateMap<UserGoal, UserGoalReadDto>()
            .ForMember(dest => dest.GoalType, opt
                => opt.MapFrom(src => src.GoalType.ToString()));
        
        CreateMap<UserGoalCreateDto, UserGoal>()
            .ForMember(dest => dest.GoalType, opt =>
                opt.MapFrom(src => Enum.Parse<Goal>(src.GoalType)));
        
        CreateMap<UserGoalUpdateDto, UserGoal>()
            .ForMember(dest => dest.GoalType, opt =>
                opt.MapFrom(src => Enum.Parse<Goal>(src.GoalType)));
    }
}