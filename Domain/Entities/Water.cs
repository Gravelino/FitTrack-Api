using Application.Abstracts;

namespace Domain.Entities;

public class WaterIntakeLog : IEntity
{
    public Guid Id { get; set; }
    public required DateTime Date { get; set; } = DateTime.UtcNow;
    public required int VolumeMl { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}