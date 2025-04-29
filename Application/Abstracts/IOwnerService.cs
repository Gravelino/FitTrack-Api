using Application.DTOs;

namespace Application.Abstracts;

public interface IOwnerService
{
    Task CreateAdminAsync(CreateStaffDto dto);
}