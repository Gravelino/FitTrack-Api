using Application.DTOs.StepsInfo;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IStepsInfoService : IService<StepsInfoReadDto, StepsInfoCreateDto, StepsInfoUpdateDto, StepsInfo>
{
    Task<IEnumerable<StepsInfoReadDto>> GetStepsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate, DateTime toDate);
}