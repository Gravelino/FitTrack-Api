using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface ITrainerRepository
{
    Task<Trainer?> GetByIdAsync(Guid userId);
    Task<IEnumerable<Trainer>> GetAllAsync();
    Task AddAsync(Trainer trainer);
    Task UpdateAsync(Trainer trainer);
    Task DeleteAsync(Trainer trainer);
    Task<IEnumerable<Trainer>> GetTrainersByGymIdAsync(Guid gymId);
}