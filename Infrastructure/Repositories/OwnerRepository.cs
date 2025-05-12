using Application.Abstracts.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly FitTrackDbContext _context;

    public OwnerRepository(FitTrackDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Owner owner)
    {
        await _context.Owners.AddAsync(owner);
    }
}