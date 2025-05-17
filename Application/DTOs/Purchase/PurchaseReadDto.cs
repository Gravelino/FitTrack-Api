using Application.Abstracts;
using Application.DTOs.Product;

namespace Application.DTOs.Purchase;

public class PurchaseReadDto: IEntity
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Guid ProductId { get; set; }
    public ProductReadDto Product { get; set; }
    public Guid UserId { get; set; }
    public Guid GymId { get; set; }
}