namespace Application.DTOs.WeightsInfo;

public class WeightsInfoCreateDto
{
    public double WeightKg { get; set; }
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
}