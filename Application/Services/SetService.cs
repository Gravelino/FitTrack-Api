using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.IndividualTraining;
using Application.DTOs.Set;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class SetService : Service<SetReadDto, SetCreateDto, SetUpdateDto, Set>, ISetService
{
    private readonly ISetRepository _repository;

    public SetService(ISetRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<SetReadDto>> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId)
    {
        var sets = await _repository.GetSetsInfoByIndividualTrainingId(individualTrainingId);
        return _mapper.Map<IEnumerable<SetReadDto>>(sets);
    }
}