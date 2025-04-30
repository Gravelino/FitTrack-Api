namespace Domain.Entities;

public class IndividualTraining : Training
{
    public Guid UserId { get; set; } 
    public User? User { get; set; }
    
    public Guid? TrainerId { get; set; }    
    public Trainer? Trainer { get; set; }
    
    public ICollection<Set>? Sets { get; set; }
}