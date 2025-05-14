using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainerCommentRepository: Repository<TrainerComment>, ITrainerCommentRepository
{
    public TrainerCommentRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TrainerComment>> GetTrainerCommentsByUserIdAndDate(Guid userId, DateTime date)
    {
        var comments = await _context.TrainerComments
            .Where(c => c.UserId == userId && c.Date.Date == date.Date)
            .ToListAsync();
        
        return comments;
    }

    public async Task<IEnumerable<TrainerComment>> GetTrainerCommentsByTrainerIdAndDate(Guid trainerId, DateTime date)
    {
        var comments = await _context.TrainerComments
            .Where(c => c.TrainerId == trainerId && c.Date.Date == date.Date)
            .ToListAsync();
        
        return comments;
    }
}