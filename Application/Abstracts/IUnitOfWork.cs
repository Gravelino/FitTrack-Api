namespace Application.Abstracts;

public interface IUnitOfWork
{
    ITrainerRepository Trainers { get; }
    IAdminRepository Admins { get; }
    Task<int> SaveChangesAsync();
}