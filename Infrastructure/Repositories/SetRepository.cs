using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SetRepository : Repository<Set>, ISetRepository
{

    public SetRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Set>> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId)
    {
        var sets = await _context.Sets
            .Where(s => s.IndividualTrainingId == individualTrainingId)
            .ToListAsync();
        
        return sets;
    }
}