using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.IndividualTraining;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class IndividualTrainingService : Service<IndividualTrainingReadDto, IndividualTrainingCreateDto,
    IndividualTrainingUpdateDto, IndividualTraining>, IIndividualTrainingService
{
    private readonly IIndividualTrainingRepository _repository;

    public IndividualTrainingService(IIndividualTrainingRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<IndividualTrainingReadDto>> GetIndividualTrainingsInfoByUserIdByPeriod(Guid userId, DateTime fromDate, DateTime toDate)
    {
        var individualTrainings = await _repository
            .GetIndividualTrainingsInfoByUserIdByPeriod(userId, fromDate, toDate);
        return _mapper.Map<IEnumerable<IndividualTrainingReadDto>>(individualTrainings);
    }
}