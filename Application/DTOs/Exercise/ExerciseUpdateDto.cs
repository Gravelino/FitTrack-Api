using Application.Abstracts;

namespace Application.DTOs.Exercise;

public class ExerciseUpdateDto : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}