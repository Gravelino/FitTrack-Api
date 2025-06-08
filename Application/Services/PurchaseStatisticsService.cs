using System.Globalization;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Purchase;
using Domain.Enums;

namespace Application.Services;

public class PurchaseStatisticsService: IPurchaseStatisticsService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUserMembershipRepository _userMembershipRepository;

    public PurchaseStatisticsService(IPurchaseRepository purchaseRepository, IUserMembershipRepository userMembershipRepository)
    {
        _purchaseRepository = purchaseRepository;
        _userMembershipRepository = userMembershipRepository;
    }

    public async Task<PurchaseStatisticsDto> GetStatisticsByOwnerIdAndPeriodAsync(Guid ownerId, DateTime from, DateTime to)
    {
        var purchases = 
            await _purchaseRepository.GetPurchasesHistoryByOwnerIdAndPeriodAsync(ownerId, from, to);
        var enumerable = purchases.ToList();
        
        var goods = enumerable.Where(p => p.Product.ProductType == ProductType.Good).ToList();
        
        var services = enumerable.Where(p => p.Product.ProductType == ProductType.Service).ToList();
        
        var memberships = 
            await _userMembershipRepository.GetUserMembershipsHistoryByOwnerIdAndPeriodAsync(ownerId, from, to);

        var groupedGoods = goods
            .GroupBy(m => m.Gym)
            .Select(g => new PurchaseStatistic(
                g.Key.Id,
                g.Key.Name,
                g.Count(),
                g.Sum(p => p.Price)
            ))
            .ToList();
        
        var groupedServices = services
            .GroupBy(m => m.Gym)
            .Select(g => new PurchaseStatistic(
                g.Key.Id,
                g.Key.Name,
                g.Count(),
                g.Sum(p => p.Price)
            ))
            .ToList();
        
        var groupedMemberships = memberships
            .GroupBy(m => m.Membership.Gym)
            .Select(g => new PurchaseStatistic(
                g.Key.Id,
                g.Key.Name,
                g.Count(),
                g.Sum(p => p.Membership.Price)
            ))
            .ToList();

        return new PurchaseStatisticsDto
        {
            Memberships = groupedMemberships,
            Services = groupedServices,
            Goods = groupedGoods
        };
    }

    public async Task<PurchaseStatisticsGroupedDto> GetStatisticsByGymIdAndPeriodAsync(Guid gymId, DateTime from, DateTime to,
        PurchasesGroupBy groupBy)
    {   
        var purchases = 
            await _purchaseRepository.GetPurchasesByGymIdAndPeriodAsync(gymId, from, to);
        var enumerable = purchases.ToList();
        
        var goods = enumerable.Where(p => p.Product.ProductType == ProductType.Good).ToList();
        
        var services = enumerable.Where(p => p.Product.ProductType == ProductType.Service).ToList();
        
        var memberships = 
            await _userMembershipRepository.GetUserMembershipsHistoryByGymIdAsync(gymId, from, to);
        
        var groupedGoods = groupBy switch
        {
            PurchasesGroupBy.Day => goods
                .GroupBy(m => m.PurchaseDate.Date)
                .Select(g => new PurchaseStatisticGrouped(
                     g.Key.ToString(CultureInfo.InvariantCulture),
                     g.Count(),
                     g.Sum(p => p.Price)
                ))
                .ToList(),

            PurchasesGroupBy.Month => goods
                .GroupBy(m => new { m.PurchaseDate.Year, m.PurchaseDate.Month })
                .Select(g => new PurchaseStatisticGrouped(
                    $"{g.Key.Year}-{g.Key.Month:D2}",
                    g.Count(),
                    g.Sum(p => p.Price)
                ))
                .ToList(),

            _ => throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null)
        };
        
        var groupedServices = groupBy switch
        {
            PurchasesGroupBy.Day => services
                .GroupBy(m => m.PurchaseDate.Date)
                .Select(g => new PurchaseStatisticGrouped(
                    g.Key.ToString(CultureInfo.InvariantCulture),
                    g.Count(),
                    g.Sum(p => p.Price)
                ))
                .ToList(),

            PurchasesGroupBy.Month => services
                .GroupBy(m => new { m.PurchaseDate.Year, m.PurchaseDate.Month })
                .Select(g => new PurchaseStatisticGrouped(
                    $"{g.Key.Year}-{g.Key.Month:D2}",
                    g.Count(),
                    g.Sum(p => p.Price)
                ))
                .ToList(),

            _ => throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null)
        };
        
        var groupedMemberships = groupBy switch
        {
            PurchasesGroupBy.Day => memberships
                .GroupBy(m => m.PurchaseDate.Date)
                .Select(g => new PurchaseStatisticGrouped(
                    g.Key.ToString(CultureInfo.InvariantCulture),
                    g.Count(),
                    g.Sum(p => p.Membership.Price)
                ))
                .ToList(),

            PurchasesGroupBy.Month => memberships
                .GroupBy(m => new { m.PurchaseDate.Year, m.PurchaseDate.Month })
                .Select(g => new PurchaseStatisticGrouped(
                    $"{g.Key.Year}-{g.Key.Month:D2}",
                    g.Count(),
                    g.Sum(p => p.Membership.Price)
                ))
                .ToList(),

            _ => throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null)
        };

        return new PurchaseStatisticsGroupedDto
        {
            Memberships = groupedMemberships,
            Services = groupedServices,
            Goods = groupedGoods,
        };
    }
}