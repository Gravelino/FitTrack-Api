using Application.Abstracts;

namespace Application.DTOs.GroupTraining;

public class GroupTrainingUpdateDto: IEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public string ContactPhone { get; set; }
    public string Description { get; set; }
    public Guid GymId { get; set; }
    public Guid TrainerId { get; set; }
}