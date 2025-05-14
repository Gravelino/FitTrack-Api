using Application.Abstracts.IRepositories;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupTrainingRepository: Repository<GroupTraining>, IGroupTrainingRepository
{
    public GroupTrainingRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<GroupTraining>> GetGroupTrainingsByTrainerIdAndPeriodAsync(Guid trainerId, DateTime fromDate, DateTime toDate)
    {
        var trainings = await _context.GroupTrainings
            .Where(t => t.TrainerId == trainerId && t.Date.Date >= fromDate.Date && t.Date.Date <= toDate.Date)
            .ToListAsync();
        
        return trainings;
    }

    public async Task<IEnumerable<GroupTraining>> GetGroupTrainingsByGymIdAndPeriodAsync(Guid gymId, DateTime fromDate, DateTime toDate)
    {
        var trainings = await _context.GroupTrainings
            .Where(t => t.GymId == gymId && t.Date.Date >= fromDate.Date && t.Date.Date <= toDate.Date)
            .ToListAsync();
        
        return trainings;
    }

    public async Task<IEnumerable<User>> GetGroupTrainingUsersByTrainingIdAsync(Guid trainingId)
    {
        var training = await _context.GroupTrainings
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == trainingId);
    
        return training?.Users ?? Enumerable.Empty<User>();
    }
}