using Application.DTOs.Gym;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Abstracts.IServices;

public interface IGymService: IService<GymReadDto, GymCreateDto, GymUpdateDto, Gym>
{
    public Task<IEnumerable<GymReadDto>> GetGymsByOwnerIdAsync(Guid ownerId);

    Task<Guid> CreateAsync(GymCreateDto dto, IFormFile mainImage);
    Task UpdateAsync(Guid id, GymUpdateDto dto, IFormFile? mainImage);
    Task<GymDetailsDto?> GetGymDetailsAsync(Guid id);
}