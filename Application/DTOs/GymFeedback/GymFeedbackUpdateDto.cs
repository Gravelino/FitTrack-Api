using Application.Abstracts;

namespace Application.DTOs.GymFeedback;

public class GymFeedbackUpdateDto: IEntity
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string? Title { get; set; }
    public string? Review { get; set; }
    public Guid UserId { get; set; }
    public Guid GymId { get; set; }
}