using Application.Abstracts;

namespace Domain.Entities;

public class Sleep : IEntity
{
    public Guid Id { get; set; }
    public required DateTime From { get; set; }
    public required DateTime To { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}