using Application.DTOs.Meal;

namespace Application.Abstracts.IServices;

public interface ICalorieStatisticsService
{
    Task<CaloriesStatisticsDto> GetStatisticsAsync(Guid userId, DateTime from, DateTime to, CaloriesGroupBy groupBy);
}