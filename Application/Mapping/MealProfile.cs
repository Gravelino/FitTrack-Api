using Application.DTOs.Meal;
using Domain.Entities;

namespace Application.Mapping;

public class MealProfile : GenericProfile<Meal, MealReadDto, MealCreateDto, MealUpdateDto>
{
    public MealProfile() : base()
    {
        
    }
}