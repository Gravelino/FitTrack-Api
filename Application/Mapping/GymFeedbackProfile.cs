using Application.DTOs.GymFeedback;
using Domain.Entities;

namespace Application.Mapping;

public class GymFeedbackProfile: GenericProfile<GymFeedbackReadDto, GymFeedbackCreateDto, GymFeedbackUpdateDto, GymFeedback>
{
    public GymFeedbackProfile(): base()
    {
        
    }
}