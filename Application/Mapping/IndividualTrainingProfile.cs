using Application.DTOs.IndividualTraining;
using Domain.Entities;

namespace Application.Mapping;

public class IndividualTrainingProfile : GenericProfile<IndividualTrainingReadDto,
    IndividualTrainingCreateDto, IndividualTrainingUpdateDto, IndividualTraining>
{
    public IndividualTrainingProfile() : base()
    {
        
    }
}