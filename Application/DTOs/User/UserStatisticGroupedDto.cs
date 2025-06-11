namespace Application.DTOs.User;

public class UserStatisticGroupedDto(string period, double totalUsers)
{
    public string Period { get; set; } = period;
    public double TotalUsers { get; set; } = totalUsers;
}
public enum UsersGroupBy
{
    Day = 1,
    Month = 2
}