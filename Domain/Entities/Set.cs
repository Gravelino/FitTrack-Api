using Application.Abstracts;

namespace Domain.Entities;

public class Set : IEntity
{
    public Guid Id { get; set; }       
    public double Weight { get; set; }  
    public int Reps { get; set; }  
    
    public Guid ExerciseId { get; set; }  
    public Exercise? Exercise { get; set; }
    
    public Guid IndividualTrainingId { get; set; }  
    public IndividualTraining? IndividualTraining { get; set; }
}