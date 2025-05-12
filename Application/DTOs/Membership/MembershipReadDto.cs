using Application.Abstracts;

namespace Application.DTOs.Membership;

public class MembershipReadDto: IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? AllowedSessions { get; set; }
    public int? DurationMonth { get; set; }
    public decimal Price { get; set; }
    public string Type { get; set; }
    public Guid GymId { get; set; }
}