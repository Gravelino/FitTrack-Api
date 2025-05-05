namespace Application.DTOs.Meal;

public class MealCreateDto
{
    public required string Name { get; set; }
    public required double Calories { get; set; }
    public required int Weight { get; set; }
    public required Guid UserId { get; set; } 
    public DateTime DateOfConsumption { get; set; }
}