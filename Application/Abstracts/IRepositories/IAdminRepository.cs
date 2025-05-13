using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IAdminRepository
{
    Task<Admin?> GetByIdAsync(Guid userId);
    Task<IEnumerable<Admin>> GetAllAsync();
    Task AddAsync(Admin admin);
    Task UpdateAsync(Admin admin);
    Task DeleteAsync(Admin admin);
    Task<IEnumerable<Admin>> GetAdminsByGymIdAsync(Guid gymId);
}