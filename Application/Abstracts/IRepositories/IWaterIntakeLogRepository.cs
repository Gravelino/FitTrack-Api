using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IWaterIntakeLogRepository : IRepository<WaterIntakeLog>
{
    public Task<IEnumerable<WaterIntakeLog>> GetByUserIdAndDayAsync(Guid userId, DateTime date);
}