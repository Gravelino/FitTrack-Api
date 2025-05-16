using Application.DTOs.Gym;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Abstracts.IServices;

public interface IGymService: IService<GymReadDto, GymCreateDto, GymUpdateDto, Gym>
{
    public Task<IEnumerable<GymReadDto>> GetGymsByOwnerIdAsync(Guid ownerId);

    Task<Guid> CreateAsync(GymCreateDto dto, IFormFile mainImage, List<IFormFile> additionalImages);
    Task UpdateAsync(Guid id, GymUpdateDto dto, IFormFile mainImage, List<IFormFile> additionalImages);
    Task<GymDetailsDto?> GetGymDetailsAsync(Guid id);
}