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
        var goodsCount = goods.Count;
        var goodsSum = goods.Sum(p => p.Price);
        
        var services = enumerable.Where(p => p.Product.ProductType == ProductType.Service).ToList();
        var servicesCount = services.Count;
        var servicesSum = services.Sum(p => p.Price);
        
        var memberships = 
            await _userMembershipRepository.GetUserMembershipsHistoryByOwnerIdAndPeriodAsync(ownerId, from, to);
        var userMemberships = memberships.ToList();
        var membershipsCount = userMemberships.Count;
        var membershipsSum = userMemberships.Sum(m => m.Membership.Price);

        return new PurchaseStatisticsDto
        {
            Memberships = new PurchaseStatistic
            {
                TotalIncome = membershipsSum,
                TotalQuantity = membershipsCount
            },

            Goods = new PurchaseStatistic
            {
                TotalIncome = goodsSum,
                TotalQuantity = goodsCount
            },

            Services = new PurchaseStatistic
            {
                TotalIncome = servicesSum,
                TotalQuantity = servicesCount
            }
        };
    }

    public async Task<PurchaseStatisticsDto> GetStatisticsByGymIdAndPeriodAsync(Guid gymId, DateTime from, DateTime to)
    {
        var purchases = 
            await _purchaseRepository.GetPurchasesByGymIdAndPeriodAsync(gymId, from, to);
        var enumerable = purchases.ToList();
        
        var goods = enumerable.Where(p => p.Product.ProductType == ProductType.Good).ToList();
        var goodsCount = goods.Count;
        var goodsSum = goods.Sum(p => p.Price);
        
        var services = enumerable.Where(p => p.Product.ProductType == ProductType.Service).ToList();
        var servicesCount = services.Count;
        var servicesSum = services.Sum(p => p.Price);
        
        var memberships = 
            await _userMembershipRepository.GetUserMembershipsHistoryByGymIdAsync(gymId, from, to);
        var userMemberships = memberships.ToList();
        var membershipsCount = userMemberships.Count;
        var membershipsSum = userMemberships.Sum(m => m.Membership.Price);

        return new PurchaseStatisticsDto
        {
            Memberships = new PurchaseStatistic
            {
                TotalIncome = membershipsSum,
                TotalQuantity = membershipsCount
            },

            Goods = new PurchaseStatistic
            {
                TotalIncome = goodsSum,
                TotalQuantity = goodsCount
            },

            Services = new PurchaseStatistic
            {
                TotalIncome = servicesSum,
                TotalQuantity = servicesCount
            }
        };
    }
}