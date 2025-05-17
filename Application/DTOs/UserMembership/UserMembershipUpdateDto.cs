using Application.Abstracts;

namespace Application.DTOs.UserMembership;

public class UserMembershipUpdateDto : IEntity
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime? ExpirationDate { get; set; }
    public int? RemainingSessions { get; set; }
    public string Status { get; set; }
    
    public Guid UserId { get; set; }
    public Guid MembershipId { get; set; }
}