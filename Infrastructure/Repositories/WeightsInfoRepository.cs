using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WeightsInfoRepository : Repository<WeightsInfo>, IWeightsInfoRepository
{
    public WeightsInfoRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WeightsInfo>> GetWeightsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
        var weights = await _context.Weights
            .Where(w => w.UserId == userId
                        && w.Date.Date >= fromDate.Date
                        && w.Date.Date <= toDate.Date)
            .ToListAsync();
        
        return weights;
    }
}