using Application.DTOs;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IOwnerService
{
    Task CreateAdminAsync(CreateStaffDto dto);
    Task CreateOwnerProfileAsync(User user);
}