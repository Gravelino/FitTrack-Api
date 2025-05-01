using Application.Abstracts;

namespace Domain.Entities;

public class Meal :IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required double Calories { get; set; }
    public required int Weight { get; set; }
    public required DateTime DateOfConsumption { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; } 
    public User User { get; set; }
}