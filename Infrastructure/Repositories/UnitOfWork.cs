using Application.Abstracts;
using Application.Abstracts.IRepositories;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly FitTrackDbContext _context;
    public ITrainerRepository Trainers { get; }
    public IAdminRepository Admins { get; }

    public UnitOfWork(FitTrackDbContext context)
    {
        _context = context;
        Trainers = new TrainerRepository(_context);
        Admins = new AdminRepository(_context);
    }
    
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}