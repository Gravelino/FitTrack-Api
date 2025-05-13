using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IGymFeedbackRepository : IRepository<GymFeedback>
{
    Task<IEnumerable<GymFeedback>> GetFeedbacksByUserIdAsync(Guid userId);
    Task<IEnumerable<GymFeedback>> GetFeedbacksByGymIdAsync(Guid gymId);
    Task<IEnumerable<GymFeedback>> GetFeedbacksByUserIdAndGymIdAsync(Guid userId, Guid gymId);
}