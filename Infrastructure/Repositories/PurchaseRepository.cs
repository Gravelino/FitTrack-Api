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

    public async Task<IEnumerable<Purchase>> GetPurchasesByGymIdAndPeriodAsync(Guid gymId, DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);
        
        var purchases = await _context.Purchases
            .Where(p => p.GymId == gymId &&
                        p.PurchaseDate.Date >= fromDate.Date &&
                        p.PurchaseDate.Date <= toDate.Date)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
        
        return purchases;
    }
}