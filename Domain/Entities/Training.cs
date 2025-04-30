namespace Domain.Entities;

public class Training
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public DateTime Date { get; set; }
}