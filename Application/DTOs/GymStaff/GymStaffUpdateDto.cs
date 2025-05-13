using System.ComponentModel.DataAnnotations;
using Application.Abstracts;

namespace Application.DTOs.GymStaff;

public class GymStaffUpdateDto: IEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
}