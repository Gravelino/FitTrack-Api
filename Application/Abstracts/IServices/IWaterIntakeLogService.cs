using Application.DTOs.WaterIntakeLog;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IWaterIntakeLogService: IService<WaterIntakeLogReadDto, WaterIntakeLogCreateDto,
    WaterIntakeLogUpdateDto, WaterIntakeLog>
{
    public Task<IEnumerable<WaterIntakeLogReadDto>> GetByUserIdAndDayAsync(Guid userId, DateTime date);
}