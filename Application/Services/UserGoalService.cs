using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.UserGoal;
using AutoMapper;
using Domain.Entities;

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
}