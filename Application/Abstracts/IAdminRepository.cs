using Domain.Entities;

namespace Application.Abstracts;

public interface IAdminRepository
{
    Task AddAsync(Admin admin);
}