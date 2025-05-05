using Application.DTOs;
using Application.DTOs.IndividualTraining;
using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IIndividualTrainingRepository : IRepository<IndividualTraining>
{
    Task<IEnumerable<IndividualTraining>> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId, DateTime fromDate,
        DateTime toDate);
}