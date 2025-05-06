using Application.DTOs.UserGoal;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IUserGoalService : IService<UserGoalReadDto, UserGoalCreateDto, UserGoalUpdateDto, UserGoal>
{
    Task<IEnumerable<UserGoalReadDto>> GetUserGoalsByUserIdAsync(Guid userId);
}