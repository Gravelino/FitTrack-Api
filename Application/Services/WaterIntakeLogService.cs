using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.WaterIntakeLog;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class WaterIntakeLogService: Service<WaterIntakeLogReadDto, WaterIntakeLogCreateDto,
    WaterIntakeLogUpdateDto, WaterIntakeLog>, IWaterIntakeLogService
{
    private readonly IWaterIntakeLogRepository _repository;

    public WaterIntakeLogService(IWaterIntakeLogRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WaterIntakeLogReadDto>> GetByUserIdAndDayAsync(Guid userId, DateTime date)
    {
        var waterIntakeLogs = await _repository.GetByUserIdAndDayAsync(userId, date);
        return _mapper.Map<IEnumerable<WaterIntakeLogReadDto>>(waterIntakeLogs);
    }
}