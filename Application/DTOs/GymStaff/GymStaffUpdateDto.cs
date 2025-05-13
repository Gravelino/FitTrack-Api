using System.ComponentModel.DataAnnotations;
using Application.Abstracts;

namespace Application.DTOs.GymStaff;

public class GymStaffUpdateDto: IEntity
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Login { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NewPassword { get; set; }
}