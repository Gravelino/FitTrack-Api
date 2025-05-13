using Application.DTOs.GymStaff;
using Microsoft.AspNetCore.Http;

namespace Application.Abstracts.IServices;

public interface IAdminService
{
    Task<IEnumerable<GymStaffReadDto>> GetAdminsByGymIdAsync(Guid gymId);
    Task<GymStaffReadDto> GetAdminByIdAsync(Guid id);
    Task<Guid> CreateAdminAsync(GymStaffCreateDto dto);
    Task UpdateAdminAsync(GymStaffUpdateDto dto);
    Task DeleteAdminByIdAsync(Guid id);
}