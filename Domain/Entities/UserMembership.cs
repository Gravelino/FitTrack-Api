using System.ComponentModel.DataAnnotations.Schema;
using Application.Abstracts;
using Domain.Enums;

namespace Domain.Entities;

public class UserMembership: IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid MembershipId { get; set; }
    public Membership Membership { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    
    public DateTime StartDate { get; set; } 
    
    private DateTime? _expirationDate;
    public DateTime? ExpirationDate 
    { 
        get => _expirationDate ?? PurchaseDate.AddMonths(Membership.DurationMonth ?? 1);
        set => _expirationDate = value;
    }

    public int? RemainingSessions { get; set; }
    
    public MembershipStatus Status { get; set; } = MembershipStatus.Pending;

    
    [NotMapped]
    public bool IsActive => DateTime.UtcNow >= StartDate &&
                            DateTime.UtcNow < ExpirationDate &&
                            RemainingSessions is null or > 0;
    
    [NotMapped]
    public bool IsExpired => Status == MembershipStatus.Expired ||
                             DateTime.UtcNow >= ExpirationDate ||
                             RemainingSessions is <= 0;
    
    [NotMapped]
    public bool IsPending => Status == MembershipStatus.Pending &&
                             DateTime.UtcNow < StartDate;
}