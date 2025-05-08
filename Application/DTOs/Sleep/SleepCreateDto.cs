namespace Application.DTOs.Sleep;

public class SleepCreateDto
{
    public DateTime SleepStart { get; set; }
    public DateTime WakeUpTime { get; set; }
    public Guid UserId { get; set; }
}