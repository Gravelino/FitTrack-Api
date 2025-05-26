using Application.DTOs.Gym;
using Application.DTOs.GymStaff;
using Application.DTOs.User;
using Application.DTOs.UserMembership;
namespace Application.Abstracts.IServices;

public interface IUserService
{
    Task<GymReadDto?> GetGymByUserId(Guid userId);
    Task<GymStaffReadDto?> GetTrainerByUserId(Guid userId);
    Task<UserMembershipReadDto?> GetActiveMembershipByGymId(Guid userId, Guid gymId);
    Task<UserMetrics> GetUserMetricsAsync(Guid userId, DateTime date);
    Task<UserDetailsDto> GetUserDetailsAsync(Guid userId, Guid gymId);
}