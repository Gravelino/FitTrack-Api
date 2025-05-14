using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainingTimeRepository: Repository<TrainingTime>, ITrainingTimeRepository
{
    public TrainingTimeRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TrainingTime>> GetTrainingTimesByUserIdTodayAsync(Guid userId)
    {
        var trainingTimes = await _context.TrainingTimes
            .Where(t => t.UserId == userId && t.Date.Date == DateTime.Today.Date)
            .ToListAsync();
        
        return trainingTimes;
    }

    public async Task<IEnumerable<TrainingTime>> GetTrainingTimesByUserIdAndPeriodAsync(Guid userId, DateTime from, DateTime to)
    {
        var trainingTimes = await _context.TrainingTimes
            .Where(t => t.UserId == userId && t.Date.Date >= from.Date && t.Date.Date <= to.Date)
            .ToListAsync();
        
        return trainingTimes;
    }
}