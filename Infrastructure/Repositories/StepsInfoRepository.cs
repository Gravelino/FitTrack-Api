using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StepsInfoRepository : Repository<StepsInfo>, IStepsInfoRepository
{
    public StepsInfoRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<StepsInfo>> GetStepsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
        var steps = await _context.Steps
            .Where(s => s.UserId == userId
                        && s.Date.Date >= fromDate.Date
                        && s.Date.Date <= toDate.Date)
           
            .ToListAsync();
        
        return steps;
    }
}