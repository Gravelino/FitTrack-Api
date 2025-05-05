using Application.Abstracts;

namespace Application.DTOs.Exercise;

public class ExerciseReadDto : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}