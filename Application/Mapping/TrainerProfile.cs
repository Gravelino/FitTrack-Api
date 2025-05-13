using Application.DTOs.GymStaff;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class TrainerProfile: Profile
{
    public TrainerProfile()
    {
        CreateMap<GymStaffCreateDto, User>()
            .ForMember(dest => dest.UserName, opt =>
                opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.PhoneNumber, opt =>
                opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.FirstName, opt =>
                opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => src.LastName))
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<GymStaffCreateDto, Trainer>()
            .ForMember(dest => dest.GymId, opt =>
                opt.MapFrom(src => src.GymId))
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<Trainer, GymStaffReadDto>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Login, opt =>
                opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.FirstName, opt =>
                opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.PhoneNumber, opt =>
                opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.GymId, opt =>
                opt.MapFrom(src => src.GymId))
            .ForMember(dest => dest.ProfileImageUrl, opt =>
                opt.MapFrom(src => src.User.PictureUrl))
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<GymStaffUpdateDto, User>()
            .ForMember(dest => dest.FirstName, opt =>
                opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt =>
                opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.UserName, opt =>
                opt.MapFrom(src => src.Login))
            .ForAllMembers(opt => opt.Ignore());
    }
}