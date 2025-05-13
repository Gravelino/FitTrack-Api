using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
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
        return _mapper.Map<IEnumerable<GymStaffReadDto>>(trainers);
    }

    public async Task<GymStaffReadDto> GetTrainerByIdAsync(Guid id)
    {
        var trainer = await _unitOfWork.Trainers.GetByIdAsync(id);
        return _mapper.Map<GymStaffReadDto>(trainer);
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

        await _userManager.AddToRoleAsync(user, IdentityRoleConstants.Trainer);

        // var trainerProfile = new Trainer
        // {
        //     Id = Guid.NewGuid(),
        //     UserId = user.Id,
        //     User = user,
        //     GymId = dto.GymId,
        // };
        var trainerProfile = _mapper.Map<Trainer>(dto);
        user.TrainerProfile = trainerProfile;
        
        if (profileImage is not null)
        {
            user.PictureUrl = await _s3Service.UploadFileAsync(profileImage, "trainers", trainerProfile.Id);
            await _userManager.UpdateAsync(user);
        }
        
        await _unitOfWork.Trainers.AddAsync(trainerProfile);
        
        return trainerProfile.Id;
    }

    public async Task UpdateTrainerAsync(GymStaffUpdateDto dto)
    {
        var trainer = await _unitOfWork.Trainers.GetByIdAsync(dto.Id);
        if (trainer is null)
            throw new KeyNotFoundException();

        _mapper.Map(dto, trainer.User);

        await _userManager.UpdateAsync(trainer.User);
    }

    public async Task UpdateTrainerWithImageAsync(GymStaffUpdateDto dto, IFormFile profileImage)
    {
        var trainer = await _unitOfWork.Trainers.GetByIdAsync(dto.Id);
        if (trainer is null)
            throw new KeyNotFoundException();

        _mapper.Map(dto, trainer.User);

        if (profileImage is not null)
        {
            trainer.User.PictureUrl = await _s3Service.UploadFileAsync(profileImage, "trainers", trainer.Id);
        }
        
        await _userManager.UpdateAsync(trainer.User);
    }

    public async Task DeleteTrainerByIdAsync(Guid id)
    {
        var existingTrainer = await _unitOfWork.Trainers.GetByIdAsync(id);
        await _unitOfWork.Trainers.DeleteAsync(existingTrainer);
    }
}