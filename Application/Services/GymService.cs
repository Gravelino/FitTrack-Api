using Amazon.S3.Model;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.Gym;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class GymService : Service<GymReadDto, GymCreateDto, GymUpdateDto, Gym>, IGymService
{
    private readonly IGymRepository _repository;
    private readonly IS3Service _s3Service;

    public GymService(IGymRepository repository, IMapper mapper, IS3Service s3Service) : base(repository, mapper)
    {
        _repository = repository;
        _s3Service = s3Service;
    }

    public async Task<IEnumerable<GymReadDto>> GetGymsByOwnerIdAsync(Guid ownerId)
    {
        var gyms = await _repository.GetGymsByOwnerIdAsync(ownerId);
        return _mapper.Map<IEnumerable<GymReadDto>>(gyms);
    }

    public async Task<Guid> CreateAsync(GymCreateDto dto, IFormFile mainImage)
    {
        var gym = _mapper.Map<Gym>(dto);
        gym.Id = Guid.NewGuid();

        if (mainImage is not null)
        {
            gym.MainImageUrl = await _s3Service.UploadFileAsync(mainImage, "gyms", gym.Id);
        }
        
        await _repository.AddAsync(gym);
        return gym.Id;
    }
    
    public async Task UpdateAsync(Guid id, GymUpdateDto dto, IFormFile? mainImage)
    {
        if (id != dto.Id)
            throw new ArgumentException("ID mismatch");

        var gym = await _repository.GetByIdAsync(id);
        if (gym == null)
            throw new KeyNotFoundException($"Gym with id {id} not found");

        _mapper.Map(dto, gym);

        if (mainImage != null)
        {
            gym.MainImageUrl = await _s3Service.UploadFileAsync(mainImage, "gyms", gym.Id);
        }

        await _repository.UpdateAsync(gym.Id, gym);
    }

    public async Task<GymDetailsDto?> GetGymDetailsAsync(Guid id)
    {
        var gym = await _repository.GetGymDetailsAsync(id);
        return _mapper.Map<GymDetailsDto>(gym);
    }
}