using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GymRepository: Repository<Gym>, IGymRepository
{
    public GymRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Gym>> GetGymsByOwnerIdAsync(Guid ownerId)
    {
        var gyms = await _context.Gyms
            .Where(g => g.OwnerId == ownerId)
            .ToListAsync();

        return gyms;
    }

    public async Task<Gym?> GetGymDetailsAsync(Guid gymId)
    {
        var gym = await _context.Gyms
            .Where(g => g.Id == gymId)
            .Include(g => g.Admins)
            .Include(g => g.Trainers)
            .Include(g => g.Feedbacks)
            .Include(g => g.Memberships)
            .Include(g => g.Products)
            .AsSplitQuery()
            .FirstOrDefaultAsync();
        
        return gym;
    }
}