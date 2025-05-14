using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface ITrainingTimeRepository: IRepository<TrainingTime>
{
    Task<IEnumerable<TrainingTime>> GetTrainingTimesByUserIdTodayAsync(Guid userId);
    Task<IEnumerable<TrainingTime>> GetTrainingTimesByUserIdAndPeriodAsync(Guid userId, DateTime from, DateTime to);
}