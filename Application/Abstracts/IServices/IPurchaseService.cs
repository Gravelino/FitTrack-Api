using Application.DTOs.Purchase;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IPurchaseService: IService<PurchaseReadDto, PurchaseCreateDto, PurchaseUpdateDto, Purchase>
{
    Task<IEnumerable<PurchaseReadDto>> GetPurchasesHistoryByUserIdAsync(Guid userId);
}