using Application.DTOs.Meal;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IMealService: IService<MealReadDto, MealCreateDto, MealUpdateDto, Meal>
{
    Task<IEnumerable<MealReadDto>> GetMealsByUserIdAndDayAsync(Guid userId, DateTime date);
}