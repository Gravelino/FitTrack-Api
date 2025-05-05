using Application.DTOs;

namespace Application.Abstracts.IServices;

public interface IOwnerService
{
    Task CreateAdminAsync(CreateStaffDto dto);
}