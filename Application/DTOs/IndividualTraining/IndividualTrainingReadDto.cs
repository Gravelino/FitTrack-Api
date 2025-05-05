using Application.Abstracts;
using Application.DTOs.Set;

namespace Application.DTOs.IndividualTraining;

public class IndividualTrainingReadDto : IEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    
    public ICollection<SetReadDto> Sets { get; set; }
}