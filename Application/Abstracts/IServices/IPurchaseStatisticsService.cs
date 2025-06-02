using Application.DTOs.Purchase;

namespace Application.Abstracts.IServices;

public interface IPurchaseStatisticsService
{
    Task<PurchaseStatisticsDto> GetStatisticsByOwnerIdAndPeriodAsync(Guid ownerId, DateTime from, DateTime to);
    Task<PurchaseStatisticsDto> GetStatisticsByGymIdAndPeriodAsync(Guid gymId, DateTime from, DateTime to);
}