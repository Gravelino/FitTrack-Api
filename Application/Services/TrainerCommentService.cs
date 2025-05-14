using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.TrainerComment;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class TrainerCommentService: Service<TrainerCommentReadDto, TrainerCommentCreateDto,
    TrainerCommentUpdateDto, TrainerComment>, ITrainerCommentService
{
    private readonly ITrainerCommentRepository _repository;

    public TrainerCommentService(ITrainerCommentRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrainerCommentReadDto>> GetTrainerCommentsByUserIdAndDate(Guid userId, DateTime date)
    {
        var comments = await _repository.GetTrainerCommentsByUserIdAndDate(userId, date);
        return _mapper.Map<IEnumerable<TrainerCommentReadDto>>(comments);
    }

    public async Task<IEnumerable<TrainerCommentReadDto>> GetTrainerCommentsByTrainerIdAndDate(Guid trainerId, DateTime date)
    {
        var comments = await _repository.GetTrainerCommentsByTrainerIdAndDate(trainerId, date);
        return _mapper.Map<IEnumerable<TrainerCommentReadDto>>(comments);
    }
}