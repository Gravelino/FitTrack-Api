using Application.Abstracts;

namespace Application.DTOs.Meal;

public class MealUpdateDto : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required double Calories { get; set; }
    public required int Weight { get; set; }
    public DateTime DateOfConsumption { get; set; }
    public required Guid UserId { get; set; } 
}