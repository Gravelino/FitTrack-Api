using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PurchaseRepository: Repository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(Guid userId)
    {
        var purchases = await _context.Purchases
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
        
        return purchases;
    }
}