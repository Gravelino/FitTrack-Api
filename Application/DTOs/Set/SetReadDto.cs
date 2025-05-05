using Application.Abstracts;
using Application.DTOs.Exercise;

namespace Application.DTOs.Set;

public class SetReadDto : IEntity
{
    public Guid Id { get; set; }
    public double Weight { get; set; }  
    public int Reps { get; set; } 
    
    public ExerciseReadDto Exercise { get; set; }
}