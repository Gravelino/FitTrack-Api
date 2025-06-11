using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.GenralStatistic;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class GeneralStatisticService: IGeneralStatisticService
{
    private static readonly DateTime OneMonthAgo = DateTime.UtcNow.AddMonths(-1).Date;
    private static readonly DateTime TwoMonthsAgo = DateTime.UtcNow.AddMonths(-2).Date;
    private static readonly DateTime Now = DateTime.UtcNow.Date;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserMembershipRepository _userMembershipRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IGymRepository _gymRepository;
    
    public GeneralStatisticService(IUnitOfWork unitOfWork, IPurchaseRepository purchaseRepository, 
        IUserMembershipRepository userMembershipRepository, IGymRepository gymRepository)
    {
        _unitOfWork = unitOfWork;
        _purchaseRepository = purchaseRepository;
        _userMembershipRepository = userMembershipRepository;
        _gymRepository = gymRepository;
    }
    
    public async Task<GeneralStatisticDto> GetStatisticsByOwnerIdAndPeriodAsync(Guid ownerId)
    {
        var generalStatisticDto = new GeneralStatisticDto();

        var purchases = await _purchaseRepository
            .GetPurchasesHistoryByOwnerIdAndPeriodAsync(ownerId, TwoMonthsAgo, Now);
        var enumerable = purchases as Purchase[] ?? purchases.ToArray();

        var memberships = await _userMembershipRepository
            .GetUserMembershipsHistoryByOwnerIdAndPeriodAsync(ownerId, TwoMonthsAgo, Now);
        var userMemberships = memberships as UserMembership[] ?? memberships.ToArray();

        generalStatisticDto.Purchases = new PurchasesStatisticDto
        {
            LastMonth = CalculateMonthlyRevenue(enumerable, userMemberships, TwoMonthsAgo, OneMonthAgo),
            CurrentMonth = CalculateMonthlyRevenue(enumerable, userMemberships, OneMonthAgo, Now)
        };

        generalStatisticDto.GymStaff = new GymStaffDto
        {
            Admins = (await _unitOfWork.Admins.GetAdminsByOwnerIdAsync(ownerId)).Count(),
            Trainers = (await _unitOfWork.Trainers.GetTrainersByOwnerIdAsync(ownerId)).Count()
        };
        
        var usersIdsWithActiveMembership = 
            await _userMembershipRepository.GetUsersIdsWithActiveMembershipsAsyncByOwnerId(ownerId);

        generalStatisticDto.Users = new UsersStatisticDto
        {
            CurrentMonth = CalculateMonthlyUniqueUsers(
                enumerable, 
                userMemberships, 
                usersIdsWithActiveMembership.Values.SelectMany(x => x),
                OneMonthAgo, 
                Now),
            LastMonth = CalculateMonthlyUniqueUsers(
                enumerable, 
                userMemberships,
                usersIdsWithActiveMembership.Values.SelectMany(x => x),
                TwoMonthsAgo, 
                OneMonthAgo)
        };
        
        generalStatisticDto.GymsCount = (await _gymRepository.GetGymsByOwnerIdAsync(ownerId)).Count();
        
        return generalStatisticDto;
    }

    public async Task<GeneralStatisticDto> GetStatisticsByGymIdAndPeriodAsync(Guid gymId)
    {
        var generalStatisticDto = new GeneralStatisticDto();

        var purchases = await _purchaseRepository
            .GetPurchasesByGymIdAndPeriodAsync(gymId, TwoMonthsAgo, Now);
        var enumerable = purchases as Purchase[] ?? purchases.ToArray();

        var memberships = await _userMembershipRepository
            .GetUserMembershipsHistoryByGymIdAsync(gymId, TwoMonthsAgo, Now);
        var userMemberships = memberships as UserMembership[] ?? memberships.ToArray();

        generalStatisticDto.Purchases = new PurchasesStatisticDto
        {
            LastMonth = CalculateMonthlyRevenue(enumerable, userMemberships, TwoMonthsAgo, OneMonthAgo),
            CurrentMonth = CalculateMonthlyRevenue(enumerable, userMemberships, OneMonthAgo, Now)
        };

        generalStatisticDto.GymStaff = new GymStaffDto
        {
            Admins = (await _unitOfWork.Admins.GetAdminsByGymIdAsync(gymId)).Count(),
            Trainers = (await _unitOfWork.Trainers.GetTrainersByGymIdAsync(gymId)).Count()
        };
        
        var usersIdsWithActiveMembership = 
            await _userMembershipRepository.GetUsersIdsWithActiveMembershipsAsyncByGymId(gymId);
        
        var idsWithActiveMembership = usersIdsWithActiveMembership as Guid[] ?? 
                                      usersIdsWithActiveMembership.ToArray();
        
        generalStatisticDto.Users = new UsersStatisticDto
        {
            CurrentMonth = CalculateMonthlyUniqueUsers(
                enumerable, 
                userMemberships, 
                idsWithActiveMembership,
                OneMonthAgo, 
                Now),
            LastMonth = CalculateMonthlyUniqueUsers(
                enumerable, 
                userMemberships,
                idsWithActiveMembership,
                TwoMonthsAgo, 
                OneMonthAgo)
        };
        
        return generalStatisticDto;
    }

    private static decimal CalculateMonthlyRevenue(
        IEnumerable<Purchase> purchases,
        IEnumerable<UserMembership> memberships,
        DateTime fromDate,
        DateTime toDate)
    {
        return purchases
            .Where(p => p.PurchaseDate >= fromDate && p.PurchaseDate <= toDate)
            .Sum(p => p.Price) +
            memberships
            .Where(m => m.PurchaseDate >= fromDate && m.PurchaseDate <= toDate)
            .Sum(p => p.Membership.Price);
    }

    private static int CalculateMonthlyUniqueUsers(
        IEnumerable<Purchase> purchases,
        IEnumerable<UserMembership> memberships,
        IEnumerable<Guid> activeUserIds,
        DateTime fromDate,
        DateTime toDate)
    {
        var uniqueUsers = new HashSet<Guid>();

        uniqueUsers.UnionWith(purchases
            .Where(p => p.PurchaseDate >= fromDate && p.PurchaseDate <= toDate)
            .Select(p => p.UserId));

        uniqueUsers.UnionWith(memberships
            .Where(m => m.PurchaseDate >= fromDate && m.PurchaseDate <= toDate)
            .Select(m => m.UserId));

        uniqueUsers.UnionWith(activeUserIds);

        return uniqueUsers.Count;
    }
}