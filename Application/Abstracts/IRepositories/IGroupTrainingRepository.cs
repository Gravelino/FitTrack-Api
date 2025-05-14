using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IGroupTrainingRepository: IRepository<GroupTraining>
{
    Task<IEnumerable<GroupTraining>> GetGroupTrainingsByTrainerIdAndPeriodAsync(Guid trainerId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<GroupTraining>> GetGroupTrainingsByGymIdAndPeriodAsync(Guid gymId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<User>> GetGroupTrainingUsersByTrainingIdAsync(Guid trainingId);
}