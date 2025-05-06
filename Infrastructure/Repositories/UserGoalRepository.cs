using Application.Abstracts.IRepositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserGoalRepository: Repository<UserGoal>, IUserGoalRepository
{
    public UserGoalRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserGoal>> GetAllUserGoalsByUserIdAsync(Guid userId)
    {
        var userGoals = await _context.UserGoals
            .Where(ug => ug.UserId == userId)
            .ToListAsync();
        
        return userGoals;
    }

    public async Task<IEnumerable<UserGoal>> GetAllUserGoalsByUserIdAndGoalTypeAsync(Guid userId,
        Goal goalType)
    {
        var userGoals = await _context.UserGoals
            .Where(ug => ug.UserId == userId && ug.GoalType == goalType)
            .ToListAsync();
        
        return userGoals;
    }
}