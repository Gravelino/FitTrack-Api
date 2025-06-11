using Application.DTOs.GenralStatistic;

namespace Application.Abstracts.IServices;

public interface IGeneralStatisticService
{
    Task<GeneralStatisticDto> GetStatisticsByOwnerIdAndPeriodAsync(Guid ownerId);
    Task<GeneralStatisticDto> GetStatisticsByGymIdAndPeriodAsync(Guid gymId);
}