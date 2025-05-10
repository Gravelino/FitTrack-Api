using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Gym;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class GymService : Service<GymReadDto, GymCreateDto, GymUpdateDto, Gym>, IGymService
{
    private readonly IGymRepository _repository;

    public GymService(IGymRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GymReadDto>> GetGymsByOwnerIdAsync(Guid ownerId)
    {
        var gyms = await _repository.GetGymsByOwnerIdAsync(ownerId);
        return _mapper.Map<IEnumerable<GymReadDto>>(gyms);
    }
}