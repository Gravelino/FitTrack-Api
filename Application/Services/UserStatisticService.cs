using System.Globalization;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Services;

public class UserStatisticService: IUserStatisticService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUserMembershipRepository _userMembershipRepository;

    public UserStatisticService(IUserMembershipRepository userMembershipService, IPurchaseRepository purchaseService)
    {
        _userMembershipRepository = userMembershipService;
        _purchaseRepository = purchaseService;
    }
    
   public async Task<List<UserStatisticDto>> GetStatisticsByOwnerIdAndPeriodAsync(Guid ownerId, DateTime from, DateTime to)
    {
        var purchases = 
            await _purchaseRepository.GetPurchasesHistoryByOwnerIdAndPeriodAsync(ownerId, from, to);
        
        var memberships = 
            await _userMembershipRepository.GetUserMembershipsHistoryByOwnerIdAndPeriodAsync(ownerId, from, to);

        var usersIdsWithActiveMembership = 
            await _userMembershipRepository.GetUsersIdsWithActiveMembershipsAsyncByOwnerId(ownerId);

        var purchasesArray = purchases as Purchase[] ?? purchases.ToArray();
        var userMembershipsArray = memberships as UserMembership[] ?? memberships.ToArray();
        
        var gymStats = userMembershipsArray
            .GroupBy(m => m.Membership.Gym)
            .ToDictionary(
                g => g.Key.Id,
                g => new UserStatisticDto(
                    g.Key.Id,
                    g.Key.Name,
                    0
                )
            );
        
        foreach (var purchase in purchasesArray)
        {
            var gymId = purchase.Gym.Id;
            if (!gymStats.ContainsKey(gymId))
            {
                gymStats[gymId] = new UserStatisticDto(
                    purchase.Gym.Id,
                    purchase.Gym.Name,
                    0
                );
            }
        }

        foreach (var gym in gymStats.Values)
        {
            var uniqueUsers = new HashSet<Guid>();
            
            uniqueUsers.UnionWith(
                userMembershipsArray
                    .Where(m => m.Membership.Gym.Id == gym.GymId)
                    .Select(m => m.UserId)
            );
            
            uniqueUsers.UnionWith(
                purchasesArray
                    .Where(p => p.Gym.Id == gym.GymId)
                    .Select(p => p.UserId)
            );

            if (usersIdsWithActiveMembership.TryGetValue(gym.GymId, out var activeUsers))
            {
                uniqueUsers.UnionWith(activeUsers);
            }

            gym.TotalUsers = uniqueUsers.Count;
        }

        return gymStats.Values.ToList();
    }

    public async Task<List<UserStatisticGroupedDto>> GetStatisticsByGymIdAndPeriodAsync(Guid gymId, DateTime from,
        DateTime to, UsersGroupBy groupBy)
    {
        var purchases = 
            await _purchaseRepository.GetPurchasesByGymIdAndPeriodAsync(gymId, from, to);
        
        var memberships = 
            await _userMembershipRepository.GetUserMembershipsHistoryByGymIdAsync(gymId, from, to);

        var usersIdsWithActiveMembership = 
            await _userMembershipRepository.GetUsersIdsWithActiveMembershipsAsyncByGymId(gymId);

        var groupedUsers = groupBy switch
        {
            UsersGroupBy.Day => GetDailyStats(memberships, purchases, usersIdsWithActiveMembership),
            UsersGroupBy.Month => GetMonthlyStats(memberships, purchases, usersIdsWithActiveMembership),
            _ => throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null)
        };

        return groupedUsers.OrderBy(x => x.Period).ToList();
    }
    
    private static List<UserStatisticGroupedDto> GetDailyStats(IEnumerable<UserMembership> memberships,
        IEnumerable<Purchase> purchases, IEnumerable<Guid> usersIdsWithActiveMembership)
    {
        return purchases
            .GroupBy(m => m.PurchaseDate.Date)
            .Select(g =>
            {
                var purchaseUserIds = g.Select(p => p.UserId).ToHashSet();
                var activeMembershipUserIds = memberships.Select(m => m.UserId).ToHashSet();

                activeMembershipUserIds.UnionWith(purchaseUserIds);
                activeMembershipUserIds.UnionWith(usersIdsWithActiveMembership);

                return new UserStatisticGroupedDto(
                    g.Key.ToString(CultureInfo.InvariantCulture),
                    activeMembershipUserIds.Count
                );
            })
            .ToList();
    }
    
    private static List<UserStatisticGroupedDto> GetMonthlyStats(IEnumerable<UserMembership> memberships,
        IEnumerable<Purchase> purchases, IEnumerable<Guid> usersIdsWithActiveMembership)
    {
        return purchases
            .GroupBy(m => new { m.PurchaseDate.Year, m.PurchaseDate.Month })
            .Select(g =>
            {
                var purchaseUserIds = g.Select(p => p.UserId).ToHashSet();
                var activeMembershipUserIds = memberships.Select(m => m.UserId).ToHashSet();

                activeMembershipUserIds.UnionWith(purchaseUserIds);
                activeMembershipUserIds.UnionWith(usersIdsWithActiveMembership);

                return new UserStatisticGroupedDto(
                    $"{g.Key.Year}-{g.Key.Month:D2}",
                    activeMembershipUserIds.Count
                );
            })
            .ToList();
    }
}