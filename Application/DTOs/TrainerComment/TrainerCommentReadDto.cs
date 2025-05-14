using Application.Abstracts;

namespace Application.DTOs.TrainerComment;

public class TrainerCommentReadDto: IEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
    public CurrentUserDto User { get; set; }
    
    public Guid TrainerId { get; set; }
}