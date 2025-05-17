using Application.Abstracts.IRepositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserMembershipRepository : Repository<UserMembership>, IUserMembershipRepository
{
    public UserMembershipRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserMembership>> GetUserMembershipsHistoryByUserIdAsync(Guid userId)
    {
        var memberships = await _context.UserMemberships
            .Where(u => u.UserId == userId)
            .ToListAsync();
        
        return memberships;
    }

    public async Task<IEnumerable<UserMembership>> GetUserActiveMembershipsByUserIdAsync(Guid userId)
    {
        var memberships = await _context.UserMemberships
            .Where(u => u.UserId == userId && u.IsActive)
            .ToListAsync();
        
        return memberships;
    }

    public async Task<UserMembership?> GetUserActiveMembershipByUserIdAndGymIdAsync(Guid userId, Guid gymId)
    {
        var membership = await _context.UserMemberships
            .Where(u => u.UserId == userId && u.IsActive && u.Membership.GymId == gymId)
            .FirstOrDefaultAsync();
        
        return membership;
    }

    public async Task<IEnumerable<UserMembership>> GetUserPendingMembershipsByUserIdAsync(Guid userId)
    {
        var memberships = await _context.UserMemberships
            .Where(u => u.UserId == userId && u.IsPending)
            .ToListAsync();
        
        return memberships;
    }

    public async Task<bool> CanAddMembershipAsync(Guid userId, Guid gymId, DateTime startDate)
    {
         var memberships = await _context.UserMemberships
             .Where(u => u.Membership.GymId == gymId && u.UserId == userId)
             .ToListAsync();
         
         var activePending = memberships.Count(m => m.IsActive || m.IsPending);
         
         return activePending < 2;
    }

    public async Task<DateTime> GetStartDateForNewMembershipAsync(Guid userId, Guid gymId)
    {
        var activeMembership = await _context.UserMemberships
            .Where(um => um.UserId == userId && 
                         um.Membership.GymId == gymId && 
                         um.Status == MembershipStatus.Active)
            .OrderByDescending(um => um.ExpirationDate)
            .FirstOrDefaultAsync();

        if (activeMembership == null)
            return DateTime.UtcNow;

        var startDate = activeMembership.ExpirationDate?.Date ?? DateTime.UtcNow;
        
        var maxStartDate = DateTime.UtcNow.AddMonths(2);
        if (startDate > maxStartDate)
            startDate = maxStartDate;

        return startDate;
    }
}