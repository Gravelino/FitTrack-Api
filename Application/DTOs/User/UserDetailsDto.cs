using System.ComponentModel.DataAnnotations;
using Application.DTOs.Gym;
using Application.DTOs.GymStaff;
using Application.DTOs.UserMembership;
using Application.DTOs.WaterIntakeLog;

namespace Application.DTOs.User;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    
    [Range(0, 500)]
    public double Weight { get; set; }
    
    [Range(0, 1440)]
    public int SleepMinutes { get; set; }
    public DailyMetricDto WaterIntake { get; set; }
    public DailyMetricDto Steps { get; set; }
    public DailyMetricDto Calories { get; set; }
    public string MembershipName { get; set; }
    //public UserMembershipReadDto? Membership { get; set; }
    public GymStaffReadDto? Trainer { get; set; }
    public GymReadDto? Gym { get; set; }
}

public class DailyMetricDto(int current, int target)
{
    public int Current { get; set; } = current;
    public int Target { get; set; } = target;
}
