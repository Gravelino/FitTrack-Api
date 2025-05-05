using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IWeightsInfoRepository : IRepository<WeightsInfo>
{
    Task<IEnumerable<WeightsInfo>> GetWeightsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate, DateTime toDate);
}