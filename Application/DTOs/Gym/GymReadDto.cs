using Application.Abstracts;
using Domain.Entities;

namespace Application.DTOs.Gym;

public class GymReadDto: IEntity
{
    public Guid Id { get; set; }
    public Address Address { get; set; }
    public string Name { get; set; }
    public double Rating { get; set; }
    public Guid OwnerId { get; set; }
    public string MainImagePreSignedUrl { get; set; }
    public ICollection<S3ObjectDto> Images { get; set; }
}