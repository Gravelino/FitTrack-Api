using Application.DTOs.Sleep;

namespace Application.Abstracts.IServices;

public interface ISleepStatisticService
{
    Task<SleepStatisticsDto> GetStatisticsAsync(Guid userId, DateTime from, DateTime to);
}