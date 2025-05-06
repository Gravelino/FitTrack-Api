using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IUserGoalRepository : IRepository<UserGoal>
{
    Task<IEnumerable<UserGoal>> GetAllUserGoalsByUserIdAsync(Guid userId);
}