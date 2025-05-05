using Application.Abstracts;

namespace Application.DTOs.WeightsInfo;

public class WeightsInfoUpdateDto : IEntity
{
    public Guid Id { get; set; }
    public double WeightKg { get; set; }
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
}