using Application.DTOs.GymStaff;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IOwnerService
{
    Task CreateOwnerProfileAsync(User user);
}