using Application.DTOs.GymFeedback;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IGymFeedbackService: IService<GymFeedbackReadDto, GymFeedbackCreateDto, GymFeedbackUpdateDto, GymFeedback>
{
    Task<IEnumerable<GymFeedbackReadDto>> GetFeedbacksByUserIdAsync(Guid userId);
    Task<IEnumerable<GymFeedbackReadDto>> GetFeedbacksByGymIdAsync(Guid gymId);
    Task<IEnumerable<GymFeedbackReadDto>> GetFeedbacksByUserIdAndGymIdAsync(Guid userId, Guid gymId);
}