using Application.Abstracts;
using Domain.Enums;

namespace Domain.Entities;

public class Product: IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }     
    public string Description { get; set; } 
    public decimal Price { get; set; }  
    public string? ImageUrl { get; set; }

    public Guid GymId { get; set; }
    public Gym? Gym { get; set; }

    public ProductType ProductType { get; set; }
}