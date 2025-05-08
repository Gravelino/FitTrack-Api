namespace Application.DTOs.Sleep;

public class SleepStatisticsDto
{
    public List<SleepGroupedDto> SleepGrouped { get; set; } = [];
    public double AverageDurationMinutes { get; set; }
}