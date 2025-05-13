using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly FitTrackDbContext _context;

    public AdminRepository(FitTrackDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetByIdAsync(Guid userId)
    {
        return await _context.Admins
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.UserId == userId);
    }

    public async Task<IEnumerable<Admin>> GetAllAsync()
    {
        return await _context.Admins
            .Include(a => a.User)
            .ToListAsync();
    }

    public async Task AddAsync(Admin admin)
    {
        await _context.Admins.AddAsync(admin);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Admin admin)
    {
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Admin admin)
    {
        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Admin>> GetAdminsByGymIdAsync(Guid gymId)
    {
        return await _context.Admins
            .Include(a => a.User)
            .Where(a => a.GymId == gymId)
            .ToListAsync();
    }
}