using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.UserGoal;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class UserGoalService: Service<UserGoalReadDto, UserGoalCreateDto, UserGoalUpdateDto, UserGoal>, IUserGoalService
{
    private readonly IUserGoalRepository _repository;

    public UserGoalService(IUserGoalRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserGoalReadDto>> GetUserGoalsByUserIdAsync(Guid userId)
    {
        var userGoals = await _repository.GetAllUserGoalsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<UserGoalReadDto>>(userGoals);
    }

    public async Task<IEnumerable<UserGoalReadDto>> GetAllUserGoalsByUserIdAndGoalTypeAsync(Guid userId, Goal goalType)
    {
        var userGoals = await _repository.GetAllUserGoalsByUserIdAndGoalTypeAsync(userId, goalType);
        return _mapper.Map<IEnumerable<UserGoalReadDto>>(userGoals);
    }
}