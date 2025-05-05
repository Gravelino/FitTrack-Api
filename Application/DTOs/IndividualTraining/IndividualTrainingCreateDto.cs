namespace Application.DTOs.IndividualTraining;

public class IndividualTrainingCreateDto
{
    public DateTime Date { get; set; }
    public Guid UserId { get; set; } 
    public Guid? TrainerId { get; set; }  
}