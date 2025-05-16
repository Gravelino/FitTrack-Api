using System.ComponentModel.DataAnnotations.Schema;
using Application.Abstracts;

namespace Domain.Entities;

public class UserMembership: IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid MembershipId { get; set; }
    public Membership Membership { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpirationDate { get => ExpirationDate; set => PurchaseDate.AddMonths(Membership.DurationMonth ?? 1); }
    public int? RemainingSessions { get; set; }
    
    [NotMapped]
    public bool IsActive => DateTime.UtcNow < ExpirationDate && RemainingSessions is null or > 0;
    
    public Guid PurchaseId { get; set; }
    public Purchase Purchase { get; set; }
}