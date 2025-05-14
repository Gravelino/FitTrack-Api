using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.GymStaff;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class TrainerService: ITrainerService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymRepository _gymRepository;
    private readonly IMapper _mapper;
    private readonly IS3Service _s3Service;

    public TrainerService(UserManager<User> userManager, IUnitOfWork unitOfWork, IGymRepository gymRepository, IMapper mapper, IS3Service s3Service)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _gymRepository = gymRepository;
        _mapper = mapper;
        _s3Service = s3Service;
    }

    public async Task<IEnumerable<GymStaffReadDto>> GetTrainersByGymIdAsync(Guid gymId)
    {
        var trainers = await _unitOfWork.Trainers.GetTrainersByGymIdAsync(gymId);
        
        return trainers.Select(t => new GymStaffReadDto
        {
            Id = t.UserId,
            GymId = t.GymId,
            FirstName = t.User.FirstName,
            LastName = t.User.LastName,
            Login = t.User.UserName ?? string.Empty,
            PhoneNumber = t.User.PhoneNumber ?? string.Empty,
            ProfileImageUrl = t.User.PictureUrl is not null ? _s3Service.GeneratePreSignedUrl(t.User.PictureUrl, TimeSpan.FromMinutes(60)) : string.Empty,
        });
    }

    public async Task<GymStaffReadDto> GetTrainerByIdAsync(Guid id)
    {
        var trainer = await _unitOfWork.Trainers.GetByIdAsync(id);

        return new GymStaffReadDto
        {
            Id = trainer.UserId,
            GymId = trainer.GymId,
            FirstName = trainer.User.FirstName,
            LastName = trainer.User.LastName,
            Login = trainer.User.UserName ?? string.Empty,
            PhoneNumber = trainer.User.PhoneNumber ?? string.Empty,
            ProfileImageUrl = trainer.User.PictureUrl is not null ? _s3Service.GeneratePreSignedUrl(trainer.User.PictureUrl, TimeSpan.FromMinutes(60)) : string.Empty,
        };
    }

    public async Task<Guid> CreateTrainerAsync(GymStaffCreateDto dto, IFormFile profileImage)
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

        await _userManager.AddToRoleAsync(user, IdentityRoleConstants.Trainer);

        var trainerProfile = new Trainer
        {
            UserId = user.Id,
            User = user,
            GymId = dto.GymId,
        };
        
        user.TrainerProfile = trainerProfile;
        
        if (profileImage is not null)
        {
            user.PictureUrl = await _s3Service.UploadFileAsync(profileImage, "trainers", trainerProfile.UserId);
            await _userManager.UpdateAsync(user);
        }
        
        await _unitOfWork.Trainers.AddAsync(trainerProfile);
        
        return trainerProfile.UserId;
    }

    public async Task UpdateTrainerAsync(GymStaffUpdateDto dto)
    {
        var trainer = await _unitOfWork.Trainers.GetByIdAsync(dto.Id);
        if (trainer is null)
            throw new KeyNotFoundException();
        
        if (!string.IsNullOrWhiteSpace(dto.Login) && dto.Login != trainer.User.UserName)
        {
            var existingUser = await _userManager.FindByNameAsync(dto.Login);
            if (existingUser != null)
                throw new UserAlreadyExistsException(dto.Login);

            trainer.User.UserName = dto.Login;
            trainer.User.NormalizedUserName = dto.Login.ToUpperInvariant();
        }

        if (!string.IsNullOrWhiteSpace(dto.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(trainer.User);
            var changePassResult = await _userManager.ResetPasswordAsync(trainer.User, token, dto.NewPassword);
            if (!changePassResult.Succeeded)
            {
                var errors = string.Join("; ", changePassResult.Errors.Select(e => e.Description));
                throw new InvalidPasswordException(errors);
            }
        }
        
        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && dto.PhoneNumber != trainer.User.PhoneNumber)
        {
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(trainer.User, dto.PhoneNumber);
            var phoneResult = await _userManager.ChangePhoneNumberAsync(trainer.User, dto.PhoneNumber, token);
            if (!phoneResult.Succeeded)
            {
                var errors = string.Join("; ", phoneResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update phone number: {errors}");
            }
        }

        if(!string.IsNullOrWhiteSpace(dto.FirstName)) trainer.User.FirstName = dto.FirstName;
        if(!string.IsNullOrWhiteSpace(dto.LastName)) trainer.User.LastName = dto.LastName;

        var updateResult = await _userManager.UpdateAsync(trainer.User);
        if (!updateResult.Succeeded)
        {
            var errors = string.Join("; ", updateResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to update user: {errors}");
        }
    }

    public async Task UpdateTrainerWithImageAsync(GymStaffUpdateDto dto, IFormFile profileImage)
    {
        var trainer = await _unitOfWork.Trainers.GetByIdAsync(dto.Id);
        if (trainer is null)
            throw new KeyNotFoundException();

        _mapper.Map(dto, trainer.User);

        if (profileImage is not null)
        {
            trainer.User.PictureUrl = await _s3Service.UploadFileAsync(profileImage, "trainers", trainer.UserId);
        }
        
        await _userManager.UpdateAsync(trainer.User);
    }

    public async Task DeleteTrainerByIdAsync(Guid id)
    {
        var existingTrainer = await _unitOfWork.Trainers.GetByIdAsync(id);
        await _unitOfWork.Trainers.DeleteAsync(existingTrainer);
    }

    public async Task<IEnumerable<CurrentUserDto>> GetTrainerClientsAsync(Guid trainerId)
    {
        var clients = await _unitOfWork.Trainers.GetClientsAsync(trainerId);
        return _mapper.Map<IEnumerable<CurrentUserDto>>(clients);
    }
}