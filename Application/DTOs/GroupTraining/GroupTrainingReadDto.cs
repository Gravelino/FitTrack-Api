using Application.Abstracts;
using Application.DTOs.Gym;
using Application.DTOs.GymStaff;

namespace Application.DTOs.GroupTraining;

public class GroupTrainingReadDto: IEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public string ContactPhone { get; set; }
    public string Description { get; set; }
    public int RegistrationsRemaining { get; set; }
    public Guid GymId { get; set; }
    public GymReadDto Gym { get; set; }
    public Guid TrainerId { get; set; }
    public GymStaffReadDto Trainer { get; set; }
}