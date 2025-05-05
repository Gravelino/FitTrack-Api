using Application.Abstracts;

namespace Application.DTOs.Meal;

public class MealReadDto : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Calories { get; set; }
    public int Weight { get; set; }
    public DateTime DateOfConsumption { get; set; }
    public Guid UserId { get; set; }
}