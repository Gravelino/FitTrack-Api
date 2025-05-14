namespace Application.DTOs.GroupTraining;

public class GroupTrainingCreateDto
{
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public string ContactPhone { get; set; }
    public string Description { get; set; }
    public int RegistrationsRemaining { get; set; }
    public Guid GymId { get; set; }
    public Guid TrainerId { get; set; }
}