using Application.Abstracts;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly FitTrackDbContext _context;

    public TrainerRepository(FitTrackDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Trainer trainer)
    {
        await _context.Trainers.AddAsync(trainer);
    }
}