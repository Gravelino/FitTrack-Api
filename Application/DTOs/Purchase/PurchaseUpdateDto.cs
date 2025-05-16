using Application.Abstracts;

namespace Application.DTOs.Purchase;

public class PurchaseUpdateDto: IEntity
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
}