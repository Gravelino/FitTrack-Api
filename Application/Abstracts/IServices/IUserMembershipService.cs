using Application.DTOs.UserMembership;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IUserMembershipService: IService<UserMembershipReadDto, UserMembershipCreateDto,
    UserMembershipUpdateDto, UserMembership>
{
    Task<IEnumerable<UserMembershipReadDto>> GetUserMembershipsHistoryByUserIdAsync(Guid userId);
    Task<IEnumerable<UserMembershipReadDto>> GetUserActiveMembershipsByUserIdAsync(Guid userId);
    Task<UserMembershipReadDto?> GetUserActiveMembershipByUserIdAndGymIdAsync(Guid userId, Guid gymId);
    Task<IEnumerable<UserMembershipReadDto>> GetUserPendingMembershipsByUserIdAsync(Guid userId);
    Task<Guid> CreateUserMembershipAsync(UserMembershipCreateDto dto);
    Task<IEnumerable<UserMembershipReadDto>> GetUserMembershipsHistoryByGymIdAsync(Guid gymId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<UserMembershipReadDto>> GetUserMembershipsHistoryByOwnerIdAndPeriodAsync(Guid ownerId, DateTime fromDate, DateTime toDate);
}