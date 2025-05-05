using Application.DTOs;

namespace Application.Abstracts.IServices;

public interface IAdminService
{
    Task CreateTrainerAsync(CreateStaffDto dto);
}