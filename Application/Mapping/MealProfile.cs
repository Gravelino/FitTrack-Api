using Application.DTOs.Meal;
using Domain.Entities;

namespace Application.Mapping;

public class MealProfile : GenericProfile<MealReadDto, MealCreateDto, MealUpdateDto, Meal>
{
    public MealProfile() : base()
    {
        
    }
}