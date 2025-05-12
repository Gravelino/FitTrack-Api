using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MembershipRepository: Repository<Membership>, IMembershipRepository
{
    public MembershipRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Membership>> GetMembershipsByGymIdAsync(Guid gymId)
    {
        var memberships = await _context.Memberships
            .Where(m => m.GymId == gymId)
            .ToListAsync();
        
        return memberships;
    }
}