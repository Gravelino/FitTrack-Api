using Domain.Entities;

namespace Application.Abstracts;

public interface ISetRepository : IRepository<Set>
{
    Task<IEnumerable<Set>> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId);
}