namespace Application.DTOs.User;

public class UserStatisticDto(Guid gymId, string gymName, int totalUsers)
{
    public Guid GymId { get; set; } = gymId;
    public string GymName { get; set; } = gymName;
    public int TotalUsers { get; set; } = totalUsers;
}