using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.Gym;
using Application.Mapping.Resolvers;
using Domain.Entities;

namespace Application.Mapping;

public class GymProfile : GenericProfile<GymReadDto, GymCreateDto, GymUpdateDto, Gym>
{
    public GymProfile()
    {
        CreateMap<GymCreateDto, Gym>()
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore())
            .ForMember(dest => dest.MainImageUrl, opt =>
                opt.Ignore())
            .ForMember(dest => dest.Images, opt =>
                opt.Ignore());
        
        CreateMap<GymUpdateDto, Gym>()
            .ForMember(dest => dest.MainImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Images, opt => opt.Ignore());

        CreateMap<Gym, GymReadDto>()
            .ForMember(dest => dest.Rating, opt =>
                opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.MainImagePreSignedUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.MainImagePreSignedUrl, opt =>
                opt.MapFrom<MainImageUrlResolver>())
            .ForMember(dest => dest.Images, opt =>
                opt.MapFrom<ImagesResolver>())
            .ForAllMembers(opt => 
                opt.Condition((src, dest, srcMember) => srcMember is not null));
        
        CreateMap<Gym, GymDetailsDto>()
            .ForMember(dest => dest.Rating, opt =>
                opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.MainImagePreSignedUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.MainImagePreSignedUrl, opt =>
                opt.MapFrom<MainImageUrlResolver>())
            .ForMember(dest => dest.Images, opt =>
                opt.MapFrom<ImagesResolver>())
            .ForAllMembers(opt => 
                opt.Condition((src, dest, srcMember) => srcMember is not null));
    }
}