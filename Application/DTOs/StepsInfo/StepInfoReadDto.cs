using Application.Abstracts;

namespace Application.DTOs.StepsInfo;

public class StepInfoReadDto : IEntity
{
    public Guid Id { get; set; }
    public int Steps { get; set; }
    public DateTime Date { get; set; }
    public Guid UserId { get; set; }
}