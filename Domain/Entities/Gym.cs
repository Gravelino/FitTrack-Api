using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Abstracts;

namespace Domain.Entities;

public class Gym : IEntity
{
    public  Guid Id { get; set; }
    public required Address Address { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    
    public required Guid OwnerId { get; set; }
    public Owner Owner { get; set; }
    
    public required string MainImageUrl { get; set; }
    public int RatingCount { get; set; }
    public int RatingSum { get; set; }

    [NotMapped]
    public double AverageRating => RatingCount == 0 ? 0 : Math.Round((double)RatingSum / RatingCount, 1);
    
    public ICollection<Trainer> Trainers { get; set; }
    public ICollection<Admin> Admins { get; set; }
    public ICollection<User> Users { get; set; }
    
    public ICollection<GroupTraining> GroupTrainings { get; set; }
    public ICollection<GymFeedback> Feedbacks { get; set; }
    public ICollection<Membership> Memberships { get; set; }
    public ICollection<Product> Products { get; set; }
}

public class Address
{
    public required string Country { get; set; }
    public required string  City { get; set; }
    public required string Street { get; set; }
    public required string Building { get; set; }
    public required string ZipCode { get; set; }
}