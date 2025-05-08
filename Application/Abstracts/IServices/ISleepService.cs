using Application.DTOs.Sleep;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface ISleepService : IService<SleepReadDto, SleepCreateDto, SleepUpdateDto, Sleep>
{
    public Task<IEnumerable<SleepReadDto>> GetSleepsByUserIdAndDayAsync(Guid userId, DateTime date);
}