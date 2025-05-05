using Application.DTOs.WeightsInfo;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IWeightsInfoService : IService<WeightsInfoReadDto, WeightsInfoCreateDto, WeightsInfoUpdateDto,
    WeightsInfo>
{
    public Task<IEnumerable<WeightsInfoReadDto>> GetWeightsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate,
        DateTime toDate);
}