using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IStepsInfoRepository : IRepository<StepsInfo>
{
    Task<IEnumerable<StepsInfo>> GetStepsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate, DateTime toDate);
}