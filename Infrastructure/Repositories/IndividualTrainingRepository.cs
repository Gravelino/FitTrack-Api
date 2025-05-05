using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IndividualTrainingRepository : Repository<IndividualTraining>, IIndividualTrainingRepository
{
    public IndividualTrainingRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<IndividualTraining>> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId,
        DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);

        var individualTrainings = await _context.IndividualTrainings
            .Include(t => t.Sets)
            .ThenInclude(s => s.Exercise)
            .Where(t => t.UserId == userId
                        && t.Date >= fromDate
                        && t.Date <= toDate)
            .ToListAsync();

        return individualTrainings;
    }
}