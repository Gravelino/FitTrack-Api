using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface ISleepRepository : IRepository<Sleep>
{
    Task<IEnumerable<Sleep>> GetSleepsByUserIdAndDayAsync(Guid userId, DateTime date);
    Task<IEnumerable<Sleep>> GetSleepsByUserIdAndPeriodAsync(Guid userId, DateTime fromDate, DateTime toDate);
}