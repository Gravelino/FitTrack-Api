using Application.DTOs.Exercise;
using Domain.Entities;

namespace Application.Mapping;

public class ExerciseProfile : GenericProfile<ExerciseReadDto, ExerciseCreateDto, ExerciseUpdateDto, Exercise>
{
    public ExerciseProfile() : base()
    {
        
    }
}