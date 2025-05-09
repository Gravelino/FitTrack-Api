namespace Application.DTOs.WaterIntakeLog;

public class WaterIntakeLogCreateDto
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int VolumeMl { get; set; }
    public Guid UserId { get; set; }
}