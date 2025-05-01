using Application.Abstracts;

namespace Domain.Entities;

public class Exercise : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
	
    public ICollection<Set> Sets { get; set; }
}