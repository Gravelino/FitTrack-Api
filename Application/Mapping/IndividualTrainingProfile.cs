using Application.DTOs.IndividualTraining;
using Domain.Entities;

namespace Application.Mapping;

public class IndividualTrainingProfile : GenericProfile<IndividualTraining, IndividualTrainingReadDto,
    IndividualTrainingCreateDto, IndividualTrainingUpdateDto>
{
    public IndividualTrainingProfile() : base()
    {
        
    }
}