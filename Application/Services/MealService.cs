using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Meal;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class MealService : Service<MealReadDto, MealCreateDto, MealUpdateDto, Meal>, IMealService
{
    private readonly IMealRepository _repository;

    public MealService(IMealRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MealReadDto>> GetMealsByUserIdAndDayAsync(Guid userId, DateTime date)
    {
        var meals = await _repository.GetMealsByUserIdAndDayAsync(userId, date);
        return _mapper.Map<IEnumerable<MealReadDto>>(meals);
    }
}