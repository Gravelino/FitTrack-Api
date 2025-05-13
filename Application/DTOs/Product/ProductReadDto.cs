using Application.Abstracts;

namespace Application.DTOs.Product;

public class ProductReadDto: IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }     
    public string Description { get; set; } 
    public decimal Price { get; set; }  
    public string? ImageUrl { get; set; }
    public string ProductType { get; set; }
    public Guid GymId { get; set; }
}