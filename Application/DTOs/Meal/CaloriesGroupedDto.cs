namespace Application.DTOs.Meal;

public record CaloriesGroupedDto(string Period, double TotalCalories);
public enum CaloriesGroupBy
{
    Day = 1,
    Month = 2
}