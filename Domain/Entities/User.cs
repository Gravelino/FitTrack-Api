using System.Collections;
using Application.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    public required string  FirstName { get; set; }
    public required string  LastName { get; set; }
    public string? PictureUrl { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }
    
    public int? Height { get; set; }
    
    public Guid? GymId { get; set; }
    public Gym? Gym { get; set; }
    public Guid? TrainerId { get; set; }
    public Trainer? Trainer { get; set; }
    
    public Owner? OwnerProfile { get; set; }
    public Trainer? TrainerProfile { get; set; }
    public Admin? AdminProfile { get; set; }
    
    public ICollection<GroupTraining> GroupTrainings { get; set; }
    public ICollection<IndividualTraining> IndividualTrainings { get; set; }

    public ICollection<Meal> Meals { get; set; }
    public ICollection<WeightsInfo> Weights { get; set; }
    public ICollection<StepsInfo> Steps { get; set; } 
    public ICollection<Sleep> Sleeps { get; set; }
    
    public ICollection<WaterIntakeLog> WaterIntakeLogs { get; set; }
    
    public ICollection<UserGoal> Goals { get; set; }
   
    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<UserMembership> UserMemberships { get; set; }
    public ICollection<TrainerComment> TrainerComments { get; set; }

    public static User Create(string email, string firstName, string lastName)
    {
        return new User
        {
            Email = email,
            UserName = email,
            FirstName = firstName,
            LastName = lastName,
        };
    }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}