using Application.Abstracts;

namespace Application.DTOs.TrainerComment;

public class TrainerCommentUpdateDto: IEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime MealDate { get; set; }
    public Guid UserId { get; set; }
    public Guid TrainerId { get; set; }
}