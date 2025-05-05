using Application.DTOs.IndividualTraining;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IIndividualTrainingService : IService<IndividualTrainingReadDto, IndividualTrainingCreateDto,
    IndividualTrainingUpdateDto, IndividualTraining>
{
    Task<IEnumerable<IndividualTrainingReadDto>> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId, DateTime fromDate,
        DateTime toDate);
}