using Domain.Entities;

namespace Application.Abstracts;

public interface IIndividualTrainingRepository : IRepository<IndividualTraining>
{
    Task<IEnumerable<IndividualTraining>> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId, DateTime fromDate,
        DateTime toDate);
}