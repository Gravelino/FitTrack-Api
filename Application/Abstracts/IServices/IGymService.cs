using Application.DTOs.Gym;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IGymService: IService<GymReadDto, GymCreateDto, GymUpdateDto, Gym>
{
    public Task<IEnumerable<GymReadDto>> GetGymsByOwnerIdAsync(Guid ownerId);
}