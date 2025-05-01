using Application.Abstracts;

namespace Domain.Entities;

public class Training : IEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
}