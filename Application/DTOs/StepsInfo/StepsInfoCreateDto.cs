namespace Application.DTOs.StepsInfo;

public class StepsInfoCreateDto
{
    public int Steps { get; set; }
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
}