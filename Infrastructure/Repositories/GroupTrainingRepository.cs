using Application.Abstracts.IRepositories;
using Application.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupTrainingRepository: Repository<GroupTraining>, IGroupTrainingRepository
{
    public GroupTrainingRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<GroupTraining>> GetGroupTrainingsByTrainerIdAndPeriodAsync(Guid trainerId, DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
        var trainings = await _context.GroupTrainings
            .Where(t => t.TrainerId == trainerId && t.Date.Date >= fromDate.Date && t.Date.Date <= toDate.Date)
            .ToListAsync();
        
        return trainings;
    }

    public async Task<IEnumerable<GroupTraining>> GetGroupTrainingsByGymIdAndPeriodAsync(Guid gymId, DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
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

    public async Task AssignUserToTrainingAsync(Guid userId, Guid trainingId)
    {
        var training = await _context.GroupTrainings
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == trainingId)
            ?? throw new NotFoundException("Training not found");

        if (training.RegistrationsRemaining <= 0)
        {
            throw new BusinessException("No available spots in this training");
        }

        var user = await _context.Users.FindAsync(userId)
            ?? throw new NotFoundException("User not found");

        if (training.Users.Any(u => u.Id == userId))
        {
            throw new BusinessException("User is already enrolled in this training");
        }

        training.Users.Add(user);
        training.RegistrationsRemaining--;
        
        await _context.SaveChangesAsync();
    }
}