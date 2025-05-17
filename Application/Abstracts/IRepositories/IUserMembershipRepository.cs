using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IUserMembershipRepository: IRepository<UserMembership>
{
    Task<IEnumerable<UserMembership>> GetUserMembershipsHistoryByUserIdAsync(Guid userId);
    Task<IEnumerable<UserMembership>> GetUserActiveMembershipsByUserIdAsync(Guid userId);
    Task<UserMembership?> GetUserActiveMembershipByUserIdAndGymIdAsync(Guid userId, Guid gymId);
    Task<IEnumerable<UserMembership>> GetUserPendingMembershipsByUserIdAsync(Guid userId);
    Task<bool> CanAddMembershipAsync(Guid userId, Guid gymId, DateTime startDate);
    Task<DateTime> GetStartDateForNewMembershipAsync(Guid userId, Guid gymId);
}