using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GymFeedbackRepository: Repository<GymFeedback>, IGymFeedbackRepository
{
    public GymFeedbackRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<GymFeedback>> GetFeedbacksByUserIdAsync(Guid userId)
    {
        return await _context.GymFeedbacks
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<GymFeedback>> GetFeedbacksByGymIdAsync(Guid gymId)
    {
        return await _context.GymFeedbacks
            .Where(f => f.GymId == gymId)
            .ToListAsync();
    }

    public async Task<IEnumerable<GymFeedback>> GetFeedbacksByUserIdAndGymIdAsync(Guid userId, Guid gymId)
    {
        return await _context.GymFeedbacks
            .Where(f => f.UserId == userId && f.GymId == gymId)
            .ToListAsync();
    }
}