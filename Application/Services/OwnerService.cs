using Application.Abstracts;
using Application.Abstracts.IServices;
using Application.DTOs;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class OwnerService : IOwnerService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;


    public OwnerService(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task CreateAdminAsync(CreateStaffDto dto)
    {
        var existingUser = await _userManager.FindByNameAsync(dto.Login);
        if (existingUser is not null)
        {
            throw new UserAlreadyExistsException(dto.Login);
        }

        var user = new User
        {
            UserName = dto.Login,
            PhoneNumber = dto.PhoneNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };
        
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.Select(e => e.Description));
        }

        await _userManager.AddToRoleAsync(user, IdentityRoleConstants.Admin);

        var adminProfile = new Admin
        {
            UserId = user.Id,
            User = user,
            GymId = dto.GymId,
        };
        
        user.AdminProfile = adminProfile;
        
        await _unitOfWork.Admins.AddAsync(adminProfile);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateOwnerProfileAsync(User user)
    {
        var ownerProfile = new Owner
        {
            UserId = user.Id,
        };
            
        user.OwnerProfile = ownerProfile;
        
        await _unitOfWork.Owners.AddAsync(ownerProfile);
        await _unitOfWork.SaveChangesAsync();
    }
}