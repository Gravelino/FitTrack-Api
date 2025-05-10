using Application.Abstracts;
using Domain.Entities;

namespace Application.DTOs.Gym;

public class GymUpdateDto: IEntity
{
    public Guid Id { get; set; }
    public Address Address { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}