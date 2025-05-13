using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.GymStaff;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymRepository _gymRepository;
    private readonly IMapper _mapper;

    public AdminService(UserManager<User> userManager, IUnitOfWork unitOfWork, IGymRepository gymRepository, IMapper mapper)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _gymRepository = gymRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GymStaffReadDto>> GetAdminsByGymIdAsync(Guid gymId)
    {
        var admins = await _unitOfWork.Admins.GetAdminsByGymIdAsync(gymId);

        return admins.Select(a => new GymStaffReadDto
        {
            Id = a.UserId,
            GymId = a.GymId,
            FirstName = a.User.FirstName,
            LastName = a.User.LastName,
            Login = a.User.UserName ?? string.Empty,
            PhoneNumber = a.User.PhoneNumber ?? string.Empty,
        });
    }

    public async Task<GymStaffReadDto> GetAdminByIdAsync(Guid id)
    {
        var admin = await _unitOfWork.Admins.GetByIdAsync(id);
        
        return new GymStaffReadDto
        {
            Id = admin.UserId,
            GymId = admin.GymId,
            FirstName = admin.User.FirstName,
            LastName = admin.User.LastName,
            Login = admin.User.UserName ?? string.Empty,
            PhoneNumber = admin.User.PhoneNumber ?? string.Empty,
        };
    }

    public async Task<Guid> CreateAdminAsync(GymStaffCreateDto dto)
    {
        var gym = await _gymRepository.GetByIdAsync(dto.GymId);
        if (gym is null)
        {
            throw new KeyNotFoundException($"Gym with id: {dto.GymId} was not found");
        }
        
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

        return adminProfile.UserId;
    }

    public async Task UpdateAdminAsync(GymStaffUpdateDto dto)
    {
        var admin = await _unitOfWork.Admins.GetByIdAsync(dto.Id);
        if (admin is null)
            throw new KeyNotFoundException();

        if (!string.IsNullOrWhiteSpace(dto.Login) && dto.Login != admin.User.UserName)
        {
            var existingUser = await _userManager.FindByNameAsync(dto.Login);
            if (existingUser != null)
                throw new UserAlreadyExistsException(dto.Login);

            admin.User.UserName = dto.Login;
            admin.User.NormalizedUserName = dto.Login.ToUpperInvariant();
        }

        if (!string.IsNullOrWhiteSpace(dto.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(admin.User);
            var changePassResult = await _userManager.ResetPasswordAsync(admin.User, token, dto.NewPassword);
            if (!changePassResult.Succeeded)
            {
                var errors = string.Join("; ", changePassResult.Errors.Select(e => e.Description));
                throw new InvalidPasswordException(errors);
            }
        }

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && dto.PhoneNumber != admin.User.PhoneNumber)
        {
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(admin.User, dto.PhoneNumber);
            var phoneResult = await _userManager.ChangePhoneNumberAsync(admin.User, dto.PhoneNumber, token);
            if (!phoneResult.Succeeded)
            {
                var errors = string.Join("; ", phoneResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update phone number: {errors}");
            }
        }

        if(!string.IsNullOrWhiteSpace(dto.FirstName)) admin.User.FirstName = dto.FirstName;
        if(!string.IsNullOrWhiteSpace(dto.LastName)) admin.User.LastName = dto.LastName;

        var updateResult = await _userManager.UpdateAsync(admin.User);
        if (!updateResult.Succeeded)
        {
            var errors = string.Join("; ", updateResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to update user: {errors}");
        }
    }

    public async Task DeleteAdminByIdAsync(Guid id)
    {
        var existingAdmin = await _unitOfWork.Admins.GetByIdAsync(id);
        await _unitOfWork.Admins.DeleteAsync(existingAdmin);
    }

    public async Task<IEnumerable<GymStaffReadDto>> GetAdminsByOwnerIdAsync(Guid ownerId)
    {
        var admins = await _unitOfWork.Admins.GetAdminsByOwnerIdAsync(ownerId);
        
        return admins.Select(a => new GymStaffReadDto
        {
            Id = a.UserId,
            GymId = a.GymId,
            FirstName = a.User.FirstName,
            LastName = a.User.LastName,
            Login = a.User.UserName ?? string.Empty,
            PhoneNumber = a.User.PhoneNumber ?? string.Empty,
        });
    }
}