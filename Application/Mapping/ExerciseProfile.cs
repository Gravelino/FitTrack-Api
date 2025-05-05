using Application.DTOs.Exercise;
using Domain.Entities;

namespace Application.Mapping;

public class ExerciseProfile : GenericProfile<Exercise, ExerciseReadDto, ExerciseCreateDto, ExerciseUpdateDto>
{
    public ExerciseProfile() : base()
    {
        
    }
}