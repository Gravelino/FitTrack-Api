using Domain.Entities;
using Domain.Enums;

namespace Application.Abstracts.IRepositories;

public interface IUserGoalRepository : IRepository<UserGoal>
{
    Task<IEnumerable<UserGoal>> GetAllUserGoalsByUserIdAsync(Guid userId);

    Task<IEnumerable<UserGoal>> GetAllUserGoalsByUserIdAndGoalTypeAsync(Guid userId, Goal goalType);
}