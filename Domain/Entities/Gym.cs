using System.ComponentModel.DataAnnotations;
using Application.Abstracts;

namespace Domain.Entities;

public class Gym : IEntity
{
    public  Guid Id { get; set; }
    public required Address Address { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    public decimal? Balance { get; set; }
    
    public required Guid OwnerId { get; set; }
    public Owner Owner { get; set; }

    public ICollection<Trainer> Trainers { get; set; }
    public ICollection<Admin> Admins { get; set; }
    public ICollection<User> Users { get; set; }
}

public class Address
{
    public required string Country { get; set; }
    public required string  City { get; set; }
    public required string Street { get; set; }
    public required string Building { get; set; }
    public required string ZipCode { get; set; }
}