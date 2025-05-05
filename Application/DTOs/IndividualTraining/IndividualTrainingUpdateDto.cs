using Application.Abstracts;

namespace Application.DTOs.IndividualTraining;

public class IndividualTrainingUpdateDto : IEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid UserId { get; set; } 
    public Guid? TrainerId { get; set; }  
}