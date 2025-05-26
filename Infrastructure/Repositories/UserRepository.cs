using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FitTrackDbContext _dbContext;

    public UserRepository(FitTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        
        return user;
    }

    public async Task<User?> GetUserDetailsAsync(Guid userId, DateTime date)
    {
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        
        return await _dbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.WaterIntakeLogs.Where(w => w.Date.Date == date.Date))
            .Include(u => u.Steps.Where(s => s.Date.Date == date.Date))
            .Include(u => u.Meals.Where(m => m.DateOfConsumption.Date == date.Date))
            .Include(u => u.Weights.OrderByDescending(w => w.Date))
            .Include(u => u.Sleeps.Where(s => s.WakeUpTime.Date == date.Date))
            .AsSplitQuery()
            .FirstOrDefaultAsync();
    }
}