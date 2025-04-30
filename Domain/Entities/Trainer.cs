using System.ComponentModel.DataAnnotations;
using Application.Abstracts;

namespace Domain.Entities;

public class Trainer : IEntity
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid GymId { get; set; }
    public Gym? Gym { get; set; }

    public ICollection<User>? Customers { get; set; } = new List<User>();
    public ICollection<IndividualTraining>? IndividualTrainings { get; set; }
    public ICollection<GroupTraining>? GroupTrainings { get; set; }
}