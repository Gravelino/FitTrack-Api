using Application.Abstracts.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MealRepository : Repository<Meal>, IMealRepository
{
    public MealRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Meal>> GetMealsByUserIdAndDayAsync(Guid userId, DateTime date)
    {
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

        var meals = await _context.Meals
            .Where(m => m.UserId == userId && m.DateOfConsumption == date)
            .ToListAsync();
        
        return meals;
    }

    public async Task<IEnumerable<Meal>> GetMealsByUserIdAndPeriodAsync(Guid userId, DateTime fromDate, DateTime toDate)
    {
        fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        toDate = DateTime.SpecifyKind(toDate, DateTimeKind.Utc);

        var meals = await _context.Meals
            .Where(m => m.UserId == userId
                        && m.DateOfConsumption >= fromDate
                        && m.DateOfConsumption <= toDate)
            .ToListAsync();

        return meals;
    }
}