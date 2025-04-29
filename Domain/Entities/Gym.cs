using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Gym
{
    public  Guid Id { get; set; }
    [MaxLength(150)]
    public required string Address { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    public decimal? Balance { get; set; }
    
    public required Guid OwnerId { get; set; }
    public Owner? Owner { get; set; }

    public ICollection<Trainer>? Trainers { get; set; }
    public ICollection<Admin>? Admins { get; set; }
    public ICollection<User>? Users { get; set; }
}