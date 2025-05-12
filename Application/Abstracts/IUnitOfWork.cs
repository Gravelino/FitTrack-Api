using Application.Abstracts.IRepositories;

namespace Application.Abstracts;

public interface IUnitOfWork
{
    ITrainerRepository Trainers { get; }
    IAdminRepository Admins { get; }
    IOwnerRepository Owners { get; }
    Task<int> SaveChangesAsync();
}