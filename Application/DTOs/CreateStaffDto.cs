using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CreateStaffDto
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [Phone]
    public required string PhoneNumber { get; set; }
    public required Guid GymId { get; set; } 
}