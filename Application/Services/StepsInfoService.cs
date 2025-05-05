using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.StepsInfo;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class StepsInfoService: Service<StepsInfoReadDto, StepsInfoCreateDto, StepsInfoUpdateDto,StepsInfo>, IStepsInfoService
{
    private readonly IStepsInfoRepository _repository;

    public StepsInfoService(IStepsInfoRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StepsInfoReadDto>> GetStepsInfoByUserIdAndPeriod(Guid userId, DateTime fromDate, DateTime toDate)
    {
        var steps = await _repository.GetStepsInfoByUserIdAndPeriod(userId, fromDate, toDate);
        return _mapper.Map<IEnumerable<StepsInfoReadDto>>(steps);
    }
}