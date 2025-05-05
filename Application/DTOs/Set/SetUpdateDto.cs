using Application.Abstracts;

namespace Application.DTOs.Set;

public class SetUpdateDto : IEntity
{
    public Guid Id { get; set; }
    public double Weight { get; set; }  
    public int Reps { get; set; }  
    public Guid ExerciseId { get; set; } 
    public Guid IndividualTrainingId { get; set; }
}