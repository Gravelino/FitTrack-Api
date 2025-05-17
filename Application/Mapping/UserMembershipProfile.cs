using Application.DTOs.UserMembership;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mapping;

public class UserMembershipProfile: GenericProfile<UserMembershipReadDto, UserMembershipCreateDto,
    UserMembershipUpdateDto, UserMembership>
{
    public UserMembershipProfile()
    {
        CreateMap<UserMembership, UserMembershipReadDto>()
            .ForMember(dest => dest.Status, opt
                => opt.MapFrom(src => src.Status.ToString()));
        
        CreateMap<UserMembershipUpdateDto, UserMembership>()
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => Enum.Parse<MembershipStatus>(src.Status)));
    }
}