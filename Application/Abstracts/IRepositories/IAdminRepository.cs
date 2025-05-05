using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IAdminRepository
{
    Task AddAsync(Admin admin);
}