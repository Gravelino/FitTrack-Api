using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SleepRepository: Repository<Sleep>, ISleepRepository
{
    public SleepRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Sleep>> GetSleepsByUserIdAndDayAsync(Guid userId, DateTime date)
    {
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        
        var sleeps = await _context.Sleeps
            .Where(s => s.UserId == userId
                        && s.WakeUpTime.Date == date.Date)
            .ToListAsync();
        
        return sleeps;
    }

    public async Task<IEnumerable<Sleep>> GetSleepsByUserIdAndPeriodAsync(Guid userId, DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
        var sleeps = await _context.Sleeps
            .Where(s => s.UserId == userId
                        && s.WakeUpTime.Date >= fromDate.Date
                        && s.WakeUpTime.Date <= toDate.Date)
            .ToListAsync();
        
        return sleeps;
    }
}