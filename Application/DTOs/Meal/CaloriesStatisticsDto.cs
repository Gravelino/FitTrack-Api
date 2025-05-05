namespace Application.DTOs.Meal;

public class CaloriesStatisticsDto
{
    public List<CaloriesGroupedDto> Items { get; set; } = [];
    public double AverageCalories { get; set; }
}