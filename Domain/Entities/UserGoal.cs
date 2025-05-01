using Application.Abstracts;
using Domain.Enums;

namespace Domain.Entities;

public class UserGoal : IEntity
{
    public Guid Id { get; set; }
    public Goal GoalType { get; set; }
    public int Value { get; set; }
    public string Unit { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}