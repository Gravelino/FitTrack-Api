using System.Collections;
using Application.DTOs.Meal;
using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IMealRepository: IRepository<Meal>
{
    Task<IEnumerable<Meal>> GetMealsByUserIdAndDayAsync(Guid userId, DateTime date);
    Task<IEnumerable<Meal>> GetMealsByUserIdAndPeriodAsync(Guid userId, DateTime fromDate, DateTime toDate);
}