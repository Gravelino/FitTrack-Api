using Application.Abstracts;
using Domain.Enums;

namespace Domain.Entities;

public class Membership: IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? AllowedSessions { get; set; }
    public int? DurationMonth { get; set; }
    public decimal Price { get; set; }
    public MembershipType Type { get; set; }
    
    public Guid GymId { get; set; }
    public Gym Gym { get; set; }
    
    public ICollection<UserMembership> UserMemberships { get; set; }
}