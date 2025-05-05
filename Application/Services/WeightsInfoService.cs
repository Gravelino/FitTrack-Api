using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.WeightsInfo;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class WeightsInfoService : Service<WeightsInfoReadDto, WeightsInfoCreateDto, WeightsInfoUpdateDto, WeightsInfo>, IWeightsInfoService
{
    private readonly IWeightsInfoRepository _repository;

    public WeightsInfoService(IWeightsInfoRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }
    

    public async Task<IEnumerable<WeightsInfoReadDto>> GetWeightsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate, DateTime toDate)
    {
        var weights = await _repository.GetWeightsInfoByUserIdAndPeriod(userId, fromDate, toDate);
        return _mapper.Map<IEnumerable<WeightsInfoReadDto>>(weights);
    }
}