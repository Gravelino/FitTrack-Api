using System.ComponentModel.DataAnnotations;
using Application.Abstracts;

namespace Application.DTOs.GymStaff;

public class GymStaffReadDto: IEntity
{
    public Guid Id { get; set; }
    public required string Login { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [Phone]
    public required string PhoneNumber { get; set; }
    public required Guid GymId { get; set; } 
    public string? ProfileImageUrl { get; set; }
}