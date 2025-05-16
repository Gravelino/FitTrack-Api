using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Purchase;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class PurchaseService: Service<PurchaseReadDto, PurchaseCreateDto, PurchaseUpdateDto, Purchase>, IPurchaseService
{
    private readonly IPurchaseRepository _repository;

    public PurchaseService(IPurchaseRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PurchaseReadDto>> GetPurchasesHistoryByUserIdAsync(Guid userId)
    {
        var purchases = await _repository.GetPurchasesByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<PurchaseReadDto>>(purchases);   
    }
}