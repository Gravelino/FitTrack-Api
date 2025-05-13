using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.GymFeedback;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class GymFeedbackService: Service<GymFeedbackReadDto, GymFeedbackCreateDto, GymFeedbackUpdateDto, GymFeedback>,
    IGymFeedbackService
{
    private readonly IGymFeedbackRepository _repository;
    private readonly IGymRepository _gymRepository;

    public GymFeedbackService(IGymFeedbackRepository repository, IMapper mapper, IGymRepository gymRepository) : base(repository, mapper)
    {
        _repository = repository;
        _gymRepository = gymRepository;
    }

    public async Task<IEnumerable<GymFeedbackReadDto>> GetFeedbacksByUserIdAsync(Guid userId)
    {
        var feedbacks = await _repository.GetFeedbacksByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<GymFeedbackReadDto>>(feedbacks);
    }

    public async Task<IEnumerable<GymFeedbackReadDto>> GetFeedbacksByGymIdAsync(Guid gymId)
    {
        var feedbacks = await _repository.GetFeedbacksByGymIdAsync(gymId);
        return _mapper.Map<IEnumerable<GymFeedbackReadDto>>(feedbacks);
    }

    public async Task<IEnumerable<GymFeedbackReadDto>> GetFeedbacksByUserIdAndGymIdAsync(Guid userId, Guid gymId)
    {
        var feedbacks = await _repository.GetFeedbacksByUserIdAndGymIdAsync(userId, gymId);
        return _mapper.Map<IEnumerable<GymFeedbackReadDto>>(feedbacks);
    }

    public override async Task<Guid> CreateAsync(GymFeedbackCreateDto dto)
    {
        var gym = await _gymRepository.GetByIdAsync(dto.GymId);
        if (gym is null)
            throw new ArgumentNullException($"gym {dto.GymId} not found");

        gym.RatingCount++;
        gym.RatingSum += dto.Rating;

        await _gymRepository.UpdateAsync(gym.Id, gym);
        
        var feedback = _mapper.Map<GymFeedback>(dto);
        var feedbackId = await _repository.AddAsync(feedback);
        
        return feedbackId;
    }
    
    public override async Task UpdateAsync(Guid id, GymFeedbackUpdateDto dto)
    {
        var gym = await _gymRepository.GetByIdAsync(dto.GymId);
        if (gym is null)
            throw new ArgumentNullException($"gym {dto.GymId} not found");

        var feedback = await _repository.GetByIdAsync(id);
        if(feedback is null)
            return;
        
        _mapper.Map(dto, feedback);
        await _repository.UpdateAsync(id, feedback);
        
        gym.RatingSum += dto.Rating - feedback.Rating;
        await _gymRepository.UpdateAsync(gym.Id, gym);
    }
    
    public override async Task DeleteAsync(Guid id)
    {
        var feedback = await _repository.GetByIdAsync(id);
        if(feedback is null)
            return;
        
        var gym = await _gymRepository.GetByIdAsync(feedback.GymId);
        if (gym is null)
            throw new ArgumentNullException($"gym {feedback.GymId} not found");

        gym.RatingCount--;
        gym.RatingSum -= feedback.Rating;
        
        await _gymRepository.UpdateAsync(gym.Id, gym);
        
        await _repository.DeleteAsync(id);
    }
}