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
}