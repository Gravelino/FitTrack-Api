using Application.DTOs;
using Application.DTOs.Set;
using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface ISetRepository : IRepository<Set>
{
    Task<IEnumerable<Set>> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId);
}