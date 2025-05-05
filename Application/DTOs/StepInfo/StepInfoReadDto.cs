using Application.Abstracts;

namespace Application.DTOs.StepInfo;

public class StepInfoReadDto : IEntity
{
    public Guid Id { get; set; }
    public int Steps { get; set; }
    public DateTime Date { get; set; }
}