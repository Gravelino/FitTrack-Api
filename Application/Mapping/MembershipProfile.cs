using Application.DTOs.Membership;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mapping;

public class MembershipProfile: GenericProfile<MembershipReadDto, MembershipCreateDto, MembershipUpdateDto, Membership>
{
    public MembershipProfile()
    {
        CreateMap<Membership, MembershipReadDto>()
            .ForMember(dest => dest.Type, opt
                => opt.MapFrom(src => src.Type.ToString()));
        
        CreateMap<MembershipCreateDto, Membership>()
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => Enum.Parse<MembershipType>(src.Type)));
        
        CreateMap<MembershipUpdateDto, Membership>()
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => Enum.Parse<MembershipType>(src.Type)));
    }
}