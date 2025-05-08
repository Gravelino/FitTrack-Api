using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Sleep;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class SleepService : Service<SleepReadDto, SleepCreateDto, SleepUpdateDto, Sleep>, ISleepService
{
    private readonly ISleepRepository _repository;

    public SleepService(ISleepRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SleepReadDto>> GetSleepsByUserIdAndDayAsync(Guid userId, DateTime date)
    {
        var sleeps = await _repository.GetSleepsByUserIdAndDayAsync(userId, date);
        return _mapper.Map<IEnumerable<SleepReadDto>>(sleeps);
    }
}