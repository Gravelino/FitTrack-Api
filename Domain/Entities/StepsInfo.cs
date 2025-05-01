using Application.Abstracts;

namespace Domain.Entities;

public class StepsInfo : IEntity
{
    public Guid Id { get; set; }
    public required int Steps { get; set; }
    public required DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}
