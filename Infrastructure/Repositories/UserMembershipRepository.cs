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
        return await _context.UserMemberships
            .Include(um => um.Membership)
            .Where(um => um.UserId == userId && 
                         um.Status == MembershipStatus.Active)
            .ToListAsync();
    }

    public async Task<UserMembership?> GetUserActiveMembershipByUserIdAndGymIdAsync(Guid userId, Guid gymId)
    {
        var membership = await _context.UserMemberships
            .Where(u => u.UserId == userId &&
                        u.Status == MembershipStatus.Active &&
                        u.Membership.GymId == gymId)
            .FirstOrDefaultAsync();
        
        return membership;
    }

    public async Task<IEnumerable<UserMembership>> GetUserPendingMembershipsByUserIdAsync(Guid userId)
    {
        var memberships = await _context.UserMemberships
            .Where(u => u.UserId == userId &&
                        u.Status == MembershipStatus.Pending
                        && DateTime.UtcNow < u.StartDate)
            .ToListAsync();
        
        return memberships;
    }

    public async Task<bool> CanAddMembershipAsync(Guid userId, Guid gymId, DateTime startDate)
    {
        var activeAndPendingCount = await _context.UserMemberships
            .Include(um => um.Membership)
            .Where(um => um.UserId == userId && 
                         um.Membership.GymId == gymId && 
                         (um.Status == MembershipStatus.Active || 
                          um.Status == MembershipStatus.Pending))
            .CountAsync();

        return activeAndPendingCount < 2;
    }

    public async Task<DateTime> GetStartDateForNewMembershipAsync(Guid userId, Guid gymId)
    {
        var activeMembership = await _context.UserMemberships
            .Include(um => um.Membership)
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

    public async Task<IEnumerable<UserMembership>> GetUserMembershipsHistoryByGymIdAsync(Guid gymId, DateTime fromDate,
        DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
        var memberships = await _context.UserMemberships
            .Where(u => u.Membership.GymId == gymId&&
                        u.PurchaseDate.Date >= fromDate.Date &&
                        u.PurchaseDate.Date <= toDate.Date)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
        
        return memberships;
    }

    public async Task<IEnumerable<UserMembership>> GetUserMembershipsHistoryByOwnerIdAndPeriodAsync(Guid ownerId,
        DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
        var memberships = await _context.UserMemberships
            .Include(u => u.Membership.Gym)
            .Where(u => u.Membership.Gym.OwnerId == ownerId &&
                        u.PurchaseDate.Date >= fromDate.Date &&
                        u.PurchaseDate.Date <= toDate.Date)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
        
        return memberships;
    }

    public async Task<IEnumerable<Guid>> GetUsersIdsWithActiveMembershipsAsyncByGymId(Guid gymId)
    {
        return await _context.UserMemberships.Where(um => 
                um.Membership.GymId == gymId && um.Status == MembershipStatus.Active)
            .Select(um => um.UserId)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Dictionary<Guid,IEnumerable<Guid>>> GetUsersIdsWithActiveMembershipsAsyncByOwnerId(Guid ownerId)
    {
        return await _context.UserMemberships.Where(um =>
                um.Membership.Gym.OwnerId == ownerId && um.Status == MembershipStatus.Active)
            .GroupBy(um => um.Membership.GymId)
            .Select(g => new
            {
                g.Key,
                Users = g.Select(um => um.UserId).Distinct()
            })
            .ToDictionaryAsync(g => g.Key, g => g.Users);;
    }
}