using Application.Abstracts;

namespace Domain.Entities;

public class Sleep : IEntity
{
    public Guid Id { get; set; }
    public required DateTime SleepStart { get; set; }
    public required DateTime WakeUpTime { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}