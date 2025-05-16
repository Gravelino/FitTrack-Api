namespace Application.DTOs.Purchase;

public class PurchaseCreateDto
{
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Guid GymId { get; set; }
}