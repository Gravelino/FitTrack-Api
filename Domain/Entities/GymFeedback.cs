using Application.Abstracts;

namespace Domain.Entities;

public class GymFeedback: IEntity
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string? Title { get; set; }
    public string? Review { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid GymId { get; set; }
    public Gym Gym { get; set; }
}