using Application.DTOs.Gym;
using Domain.Entities;

namespace Application.Mapping;

public class GymProfile : GenericProfile<GymReadDto, GymCreateDto, GymUpdateDto, Gym>
{
    public GymProfile(): base()
    {
        CreateMap<Gym, GymReadDto>()
            .ForMember(dest => dest.Rating, opt =>
                opt.MapFrom(src => src.AverageRating));
    }
}