using Application.Abstracts;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly FitTrackDbContext _context;

    public AdminRepository(FitTrackDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Admin admin)
    {
        await _context.Admins.AddAsync(admin);
    }
}