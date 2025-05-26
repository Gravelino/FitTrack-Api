using Application.DTOs.GroupTraining;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IGroupTrainingService: IService<GroupTrainingReadDto, GroupTrainingCreateDto, 
    GroupTrainingUpdateDto, GroupTraining>
{
    Task<IEnumerable<GroupTrainingReadDto>> GetGroupTrainingsByTrainerIdAndPeriodAsync(Guid trainerId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<GroupTrainingReadDto>> GetGroupTrainingsByGymIdAndPeriodAsync(Guid gymId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<CurrentUserDto>> GetGroupTrainingUsersByTrainingIdAsync(Guid trainingId);
    Task AssignUserToTrainingAsync(Guid userId, Guid trainingId);
    Task<IEnumerable<GroupTrainingReadDto>> GetUserGroupTrainingsHistoryAsync(Guid userId);
}