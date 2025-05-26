using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, CurrentUserDto>()
            .ForMember(dest => dest.Login, opt =>
                opt.MapFrom(src => src.Email));
        // .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<UserImageResolver>());
    }
}