using Application.DTOs.User;

namespace Application.Abstracts.IServices;

public interface IUserStatisticService
{
    Task<List<UserStatisticDto>> GetStatisticsByOwnerIdAndPeriodAsync(Guid ownerId, DateTime from, DateTime to);
    Task<List<UserStatisticGroupedDto>> GetStatisticsByGymIdAndPeriodAsync(Guid gymId, DateTime from, DateTime to, UsersGroupBy groupBy);
}