using Application.Abstracts;
using Application.DTOs;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }
    
    public async Task CreateTrainerAsync(CreateStaffDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser is not null)
        {
            throw new UserAlreadyExistsException(dto.Email);
        }
        
        var user =  User.Create(dto.Email, dto.FirstName, dto.LastName);
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.Select(e => e.Description));
        }

        await _userManager.AddToRoleAsync(user, IdentityRoleConstants.Trainer);

        var trainerProfile = new Trainer
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            User = user,
            GymId = dto.GymId,
        };
        
        user.TrainerProfile = trainerProfile;
        
        await _unitOfWork.Trainers.AddAsync(trainerProfile);
        await _unitOfWork.SaveChangesAsync();
    }
}