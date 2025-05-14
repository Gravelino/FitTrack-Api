using Application.Abstracts;

namespace Domain.Entities;

public class TrainingTime: IEntity
{
    public Guid Id { get; set; }
    public int DurationSeconds { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}