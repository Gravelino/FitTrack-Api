using Application.Abstracts;

namespace Domain.Entities;

public class Purchase: IEntity
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    
    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }
    
    public Guid? MembershipId { get; set; }
    public Membership? Membership { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid GymId { get; set; }
    public Gym Gym { get; set; }
}