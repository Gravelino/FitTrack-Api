using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IPurchaseRepository: IRepository<Purchase>
{
    Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(Guid userId);
}