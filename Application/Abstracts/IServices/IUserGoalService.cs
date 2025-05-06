using Application.DTOs.UserGoal;
using Domain.Entities;
using Domain.Enums;

namespace Application.Abstracts.IServices;

public interface IUserGoalService : IService<UserGoalReadDto, UserGoalCreateDto, UserGoalUpdateDto, UserGoal>
{
    public Task<IEnumerable<UserGoalReadDto>> GetUserGoalsByUserIdAsync(Guid userId);

    public Task<IEnumerable<UserGoalReadDto>> GetAllUserGoalsByUserIdAndGoalTypeAsync(Guid userId,
        Goal goalType);
}