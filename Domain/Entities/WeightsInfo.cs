using Application.Abstracts;

namespace Domain.Entities;

public class WeightsInfo : IEntity
{
    public Guid Id { get; set; }
    public required double WeightKg { get; set; }
    public required DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}