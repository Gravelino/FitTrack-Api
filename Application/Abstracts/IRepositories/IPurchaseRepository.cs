using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IPurchaseRepository: IRepository<Purchase>
{
    Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(Guid userId);
    Task<IEnumerable<Purchase>> GetPurchasesByGymIdAndPeriodAsync(Guid gymId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<Purchase>> GetPurchasesHistoryByOwnerIdAndPeriodAsync(Guid ownerId, DateTime fromDate, DateTime toDate);
}