using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.GymStaff;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        return _mapper.Map<IEnumerable<GymStaffReadDto>>(admins);
    }

    public async Task<GymStaffReadDto> GetAdminByIdAsync(Guid id)
    {
        var admin = await _unitOfWork.Admins.GetByIdAsync(id);
        return _mapper.Map<GymStaffReadDto>(admin);
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

        // var user = new User
        // {
        //     UserName = dto.Login,
        //     PhoneNumber = dto.PhoneNumber,
        //     FirstName = dto.FirstName,
        //     LastName = dto.LastName,
        // };
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.Select(e => e.Description));
        }

        await _userManager.AddToRoleAsync(user, IdentityRoleConstants.Admin);

        // var adminProfile = new Admin
        // {
        //     UserId = user.Id,
        //     User = user,
        //     GymId = dto.GymId,
        // };
        var adminProfile = _mapper.Map<Admin>(dto);
        user.AdminProfile = adminProfile;
        
        await _unitOfWork.Admins.AddAsync(adminProfile);

        return adminProfile.UserId;
    }

    public async Task UpdateAdminAsync(GymStaffUpdateDto dto)
    {
        var admin = await _unitOfWork.Admins.GetByIdAsync(dto.Id);
        if (admin is null)
            throw new KeyNotFoundException();

        _mapper.Map(dto, admin.User);

        await _userManager.UpdateAsync(admin.User);
    }

    public async Task DeleteAdminByIdAsync(Guid id)
    {
        var existingAdmin = await _unitOfWork.Admins.GetByIdAsync(id);
        await _unitOfWork.Admins.DeleteAsync(existingAdmin);
    }
}