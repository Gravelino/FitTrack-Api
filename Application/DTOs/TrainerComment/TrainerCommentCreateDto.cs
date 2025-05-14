namespace Application.DTOs.TrainerComment;

public class TrainerCommentCreateDto
{
    public string Message { get; set; }
    public Guid UserId { get; set; }
    public Guid TrainerId { get; set; }
}