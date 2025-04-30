using Application.Abstracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IndividualTrainingRepository : Repository<IndividualTraining>, IIndividualTrainingRepository
{
    private readonly FitTrackDbContext _context;

    public IndividualTrainingRepository(FitTrackDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IndividualTraining>> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId,
        DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);

        var individualTrainings = await _context.IndividualTrainings
            .Where(t => t.UserId == userId
                        && t.Date >= fromDate
                        && t.Date <= toDate)
            .ToListAsync();
        
        return individualTrainings;
    }
}