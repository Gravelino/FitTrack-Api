namespace Domain.Entities;

public class UserMembership
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid MembershipId { get; set; }
    public Membership Membership { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpirationDate { get => ExpirationDate; set => PurchaseDate.AddMonths(Membership.DurationMonth ?? 1); }
    public int? RemainingSessions { get; set; }
}