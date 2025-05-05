using Application.Abstracts;

namespace Application.DTOs.UserGoal;

public class UserGoalReadDto: IEntity
{
    public Guid Id { get; set; }
    public string GoalType { get; set; }
    public int Value { get; set; }
}