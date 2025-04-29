using Application.DTOs;

namespace Application.Abstracts;

public interface IAdminService
{
    Task CreateTrainerAsync(CreateStaffDto dto);
}