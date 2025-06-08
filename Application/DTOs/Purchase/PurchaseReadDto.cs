using Application.Abstracts;
using Application.DTOs.Gym;
using Application.DTOs.Product;
using Application.DTOs.User;

namespace Application.DTOs.Purchase;

public class PurchaseReadDto: IEntity
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Guid ProductId { get; set; }
    public ProductReadDto Product { get; set; }
    public Guid UserId { get; set; }
    public CurrentUserDto User { get; set; }   
    public Guid GymId { get; set; }
    public GymReadDto Gym { get; set; }   
}