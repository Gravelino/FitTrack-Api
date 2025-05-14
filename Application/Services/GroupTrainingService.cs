using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.GroupTraining;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class GroupTrainingService: Service<GroupTrainingReadDto, GroupTrainingCreateDto, 
    GroupTrainingUpdateDto, GroupTraining>, IGroupTrainingService
{
    private readonly IGroupTrainingRepository _repository;

    public GroupTrainingService(IGroupTrainingRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GroupTrainingReadDto>> GetGroupTrainingsByTrainerIdAndPeriodAsync(Guid trainerId, DateTime fromDate, DateTime toDate)
    {
        var groupTrainings = await _repository.GetGroupTrainingsByTrainerIdAndPeriodAsync(trainerId, fromDate, toDate);
        return _mapper.Map<IEnumerable<GroupTrainingReadDto>>(groupTrainings);
    }

    public async Task<IEnumerable<GroupTrainingReadDto>> GetGroupTrainingsByGymIdAndPeriodAsync(Guid gymId, DateTime fromDate, DateTime toDate)
    {
        var groupTrainings = await _repository.GetGroupTrainingsByGymIdAndPeriodAsync(gymId, fromDate, toDate);
        return _mapper.Map<IEnumerable<GroupTrainingReadDto>>(groupTrainings);
    }

    public async Task<IEnumerable<CurrentUserDto>> GetGroupTrainingUsersByTrainingIdAsync(Guid trainingId)
    {
        var users = await _repository.GetGroupTrainingUsersByTrainingIdAsync(trainingId);
        return _mapper.Map<IEnumerable<CurrentUserDto>>(users);   
    }

    public async Task AssignUserToTrainingAsync(Guid userId, Guid trainingId)
    {
        await _repository.AssignUserToTrainingAsync(userId, trainingId);
    }
}