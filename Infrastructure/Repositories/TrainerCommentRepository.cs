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
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc); 
        
        var comments = await _context.TrainerComments
            .Where(c => c.UserId == userId && c.MealDate.Date == date.Date)
            .ToListAsync();
        
        return comments;
    }

    public async Task<IEnumerable<TrainerComment>> GetTrainerCommentsByTrainerIdAndDate(Guid trainerId, DateTime date)
    {
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        
        var comments = await _context.TrainerComments
            .Where(c => c.TrainerId == trainerId && c.MealDate.Date == date.Date)
            .ToListAsync();
        
        return comments;
    }
}