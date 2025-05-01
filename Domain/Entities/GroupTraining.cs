namespace Domain.Entities;

public class GroupTraining : Training
{
    public required int DurationInMinutes { get; set; }
    public required string ContactPhone { get; set; }
    public required string Description { get; set; }
	
    public Guid GymId { get; set; }
    public Gym Gym { get; set; }
	
    public Guid TrainerId { get; set; }
    public Trainer Trainer { get; set; }

    public ICollection<User> Users { get; set; }
}