using Application.DTOs.Meal;
using Domain.Entities;

namespace Application.Mapping;

public class MealProfile : GenericProfile<MealCreateDto, MealReadDto, MealUpdateDto, Meal>
{
    public MealProfile() : base()
    {
        
    }
}