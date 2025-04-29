using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Owner
{
    [Key]
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    //public ICollection<Gym>? Gyms { get; set; }
}