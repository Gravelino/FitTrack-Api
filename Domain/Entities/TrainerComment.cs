using Application.Abstracts;

namespace Domain.Entities;

public class TrainerComment: IEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid TrainerId { get; set; }
    public Trainer Trainer { get; set; }
}