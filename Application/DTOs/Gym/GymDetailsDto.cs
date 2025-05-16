using Application.DTOs.GymFeedback;
using Application.DTOs.GymStaff;
using Application.DTOs.Membership;
using Application.DTOs.Product;

namespace Application.DTOs.Gym;

public class GymDetailsDto: GymReadDto
{
    public ICollection<GymStaffReadDto> Admins { get; set; }
    public ICollection<GymStaffReadDto> Trainers { get; set; }
    public ICollection<GymFeedbackReadDto> Feedbacks { get; set; }
    public ICollection<ProductReadDto> Products { get; set; }
    public ICollection<MembershipReadDto> Memberships { get; set; }
}