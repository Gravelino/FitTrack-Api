using Application.DTOs.GymStaff;
using Microsoft.AspNetCore.Http;

namespace Application.Abstracts.IServices;

public interface ITrainerService
{
    Task<IEnumerable<GymStaffReadDto>> GetTrainersByGymIdAsync(Guid gymId);
    Task<GymStaffReadDto> GetTrainerByIdAsync(Guid id);
    Task<Guid> CreateTrainerAsync(GymStaffCreateDto dto, IFormFile profileImage);
    Task UpdateTrainerAsync(GymStaffUpdateDto dto);
    Task UpdateTrainerWithImageAsync(GymStaffUpdateDto dto, IFormFile profileImage);
    Task DeleteTrainerByIdAsync(Guid id);
}