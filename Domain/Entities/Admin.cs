using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Admin
{
    [Key]
    public Guid UserId { get; set; }
    public User User { get; set; }
    
   // public int GymId { get; set; }
    //public Gym? Gym { get; set; }
}