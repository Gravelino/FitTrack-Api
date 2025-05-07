using System.Globalization;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Meal;

namespace Application.Services;

public class CalorieStatisticsService : ICalorieStatisticsService
{
    private readonly IMealRepository _repository;

    public CalorieStatisticsService(IMealRepository repository)
    {
        _repository = repository;
    }

    public async Task<CaloriesStatisticsDto> GetStatisticsAsync(Guid userId, DateTime from, DateTime to,
        CaloriesGroupBy groupBy)
    {
        var meals = await _repository.GetMealsByUserIdAndPeriodAsync(userId, from, to);

        var groupedData = groupBy switch
        {
            CaloriesGroupBy.Day => meals
                .GroupBy(m => m.DateOfConsumption.Date)
                .Select(g => new CaloriesGroupedDto(
                    g.Key.ToString(CultureInfo.InvariantCulture),
                    g.Sum(m => m.Calories)))
                .ToList(),

            CaloriesGroupBy.Month => meals
                .GroupBy(m => new { m.DateOfConsumption.Year, m.DateOfConsumption.Month })
                .Select(g => new CaloriesGroupedDto
                (
                    $"{g.Key.Year}-{g.Key.Month:D2}",
                    g.Sum(m => m.Calories)
                ))
                .ToList(),

            _ => throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null)
        };
        
        var averageCalories = groupedData.Any() ? groupedData.Average(c => c.TotalCalories) : 0;

        return new CaloriesStatisticsDto
        {
            Items = groupedData,
            AverageCalories = averageCalories,
        };
    }
}