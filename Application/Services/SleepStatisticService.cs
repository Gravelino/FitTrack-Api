using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Sleep;

namespace Application.Services;

public class SleepStatisticService: ISleepStatisticService
{
    private readonly ISleepRepository _repository;

    public SleepStatisticService(ISleepRepository repository)
    {
        _repository = repository;
    }

    public async Task<SleepStatisticsDto> GetStatisticsAsync(Guid userId, DateTime from, DateTime to)
    {
        var sleeps = await _repository.GetSleepsByUserIdAndPeriodAsync(userId, from, to);

        var groupedData = sleeps.GroupBy(s => s.WakeUpTime.Date)
            .Select(g => new SleepGroupedDto
            {
                Date = g.Key,
                DurationMinutes = g.Sum(s => (int)(s.WakeUpTime - s.SleepStart).TotalMinutes)
            })
            .OrderBy(s => s.Date)
            .ToList();
        
        var averageDurationMinutes = groupedData.Any() ? groupedData.Average(s => s.DurationMinutes) : 0;

        return new SleepStatisticsDto
        {
            SleepGrouped = groupedData,
            AverageDurationMinutes = averageDurationMinutes
        };
    }
}