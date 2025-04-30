using Domain.Entities;

namespace Application.Abstracts;

public interface ISetRepository
{
    Task<IEnumerable<Set>> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId);
}