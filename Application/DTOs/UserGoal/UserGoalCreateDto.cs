namespace Application.DTOs.UserGoal;

public class UserGoalCreateDto
{
    public string GoalType { get; set; }
    public int Value { get; set; }
    public Guid UserId { get; set; }
}