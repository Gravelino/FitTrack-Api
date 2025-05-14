using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface ITrainerCommentRepository: IRepository<TrainerComment>
{
    Task<IEnumerable<TrainerComment>> GetTrainerCommentsByUserIdAndDate(Guid userId, DateTime date);
    Task<IEnumerable<TrainerComment>> GetTrainerCommentsByTrainerIdAndDate(Guid trainerId, DateTime date);
}