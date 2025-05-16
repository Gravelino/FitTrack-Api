using Application.DTOs.Purchase;
using Domain.Entities;

namespace Application.Mapping;

public class PurchaseProfile: GenericProfile<PurchaseReadDto, PurchaseCreateDto, PurchaseUpdateDto, Purchase>
{
    public PurchaseProfile(): base()
    {
        
    }
}