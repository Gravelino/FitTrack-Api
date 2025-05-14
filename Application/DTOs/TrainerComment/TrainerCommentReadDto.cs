using Application.Abstracts;
using Application.DTOs.GymStaff;

namespace Application.DTOs.TrainerComment;

public class TrainerCommentReadDto: IEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime MealDate { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid TrainerId { get; set; }
    public GymStaffReadDto Trainer { get; set; }
}