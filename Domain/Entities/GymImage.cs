using Application.Abstracts;

namespace Domain.Entities;

public class GymImage: IEntity
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }
    
    public Guid GymId { get; set; }
    public Gym Gym { get; set; }
}