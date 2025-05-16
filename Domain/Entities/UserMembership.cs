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
    private DateTime? _expirationDate;
    public DateTime? ExpirationDate 
    { 
        get => _expirationDate ?? PurchaseDate.AddMonths(Membership.DurationMonth ?? 1);
        set => _expirationDate = value;
    }

    public int? RemainingSessions { get; set; }
    
    [NotMapped]
    public bool IsActive => DateTime.UtcNow < ExpirationDate && RemainingSessions is null or > 0;
}