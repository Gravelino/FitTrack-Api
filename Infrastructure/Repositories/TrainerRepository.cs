using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly FitTrackDbContext _context;

    public TrainerRepository(FitTrackDbContext context)
    {
        _context = context;
    }

    public async Task<Trainer?> GetByIdAsync(Guid id)
    {
        return await _context.Trainers
            .Include(t => t.User)
            .Include(t => t.Customers)
            .FirstOrDefaultAsync(t => t.UserId == id);
    }

    public async Task<IEnumerable<Trainer>> GetAllAsync()
    {
        return await _context.Trainers
            .Include(t => t.User)
            .ToListAsync();
    }

    public async Task AddAsync(Trainer trainer)
    {
        await _context.Trainers.AddAsync(trainer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Trainer trainer)
    {
        _context.Trainers.Update(trainer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Trainer trainer)
    {
        _context.Trainers.Remove(trainer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Trainer>> GetTrainersByGymIdAsync(Guid gymId)
    {
        var trainers = await _context.Trainers
            .Where(t => t.GymId == gymId)
            .Include(t => t.User)
            .Include(t => t.Customers)
            .ToListAsync();
        
        return trainers;
    }

    public async Task<IEnumerable<Trainer>> GetTrainersByOwnerIdAsync(Guid ownerId)
    {
        var trainers = await _context.Trainers
            .Include(t => t.Gym)
            .Where(t => t.Gym.OwnerId == ownerId)
            .Include(t => t.User)
            .Include(t => t.Customers)
            .ToListAsync();
        
        return trainers;
    }

    public async Task<IEnumerable<User>> GetClientsAsync(Guid trainerId)
    {
        var clients = await _context.Users
            .Where(u => u.TrainerId == trainerId)
            .ToListAsync();
        
        return clients;
    }
}