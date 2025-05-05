namespace Application.DTOs.Set;

public class SetCreateDto
{
    public double Weight { get; set; }  
    public int Reps { get; set; }  
    public Guid ExerciseId { get; set; } 
    public Guid IndividualTrainingId { get; set; }
}