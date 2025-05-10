using Domain.Entities;

namespace Application.DTOs.Gym;

public class GymCreateDto
{
    public Address Address { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}