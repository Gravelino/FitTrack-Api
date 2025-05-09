using Application.Abstracts;

namespace Application.DTOs.WaterIntakeLog;

public class WaterIntakeLogUpdateDto : IEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int VolumeMl { get; set; }
    public Guid UserId { get; set; }
}