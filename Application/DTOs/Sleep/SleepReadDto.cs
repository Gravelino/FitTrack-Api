using Application.Abstracts;

namespace Application.DTOs.Sleep;

public class SleepReadDto : IEntity
{
    public Guid Id { get; set; }
    public DateTime SleepStart { get; set; }
    public DateTime WakeUpTime { get; set; }
    public Guid UserId { get; set; }
}