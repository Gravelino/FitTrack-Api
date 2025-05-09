using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WaterIntakeLogRepository : Repository<WaterIntakeLog>, IWaterIntakeLogRepository
{
    public WaterIntakeLogRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WaterIntakeLog>> GetByUserIdAndDayAsync(Guid userId, DateTime date)
    {
        var waterIntakeLogs = await _context.WaterIntakeLogs
            .Where(w => w.UserId == userId && date.Date == w.Date)
            .ToListAsync();
        
        return waterIntakeLogs;
    }
}