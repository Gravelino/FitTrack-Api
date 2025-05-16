using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IGymRepository : IRepository<Gym>
{
    Task<IEnumerable<Gym>> GetGymsByOwnerIdAsync(Guid ownerId);
    Task<Gym?> GetGymDetailsAsync(Guid gymId);
}