using System.ComponentModel.DataAnnotations;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.UserMembership;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Services;

public class UserMembershipService: Service<UserMembershipReadDto, UserMembershipCreateDto, UserMembershipUpdateDto,
    UserMembership>, IUserMembershipService
{
    private readonly IUserMembershipRepository _repository;
    private readonly IMembershipRepository _membershipRepository;

    public UserMembershipService(IUserMembershipRepository repository, IMapper mapper, IMembershipRepository membershipRepository) : base(repository, mapper)
    {
        _repository = repository;
        _membershipRepository = membershipRepository;
    }

    public async Task<IEnumerable<UserMembershipReadDto>> GetUserMembershipsHistoryByUserIdAsync(Guid userId)
    {
        var memberships = await _repository.GetUserMembershipsHistoryByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<UserMembershipReadDto>>(memberships);   
    }

    public async Task<IEnumerable<UserMembershipReadDto>> GetUserActiveMembershipsByUserIdAsync(Guid userId)
    {
        var memberships = await _repository.GetUserActiveMembershipsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<UserMembershipReadDto>>(memberships);  
    }

    public async Task<UserMembershipReadDto?> GetUserActiveMembershipByUserIdAndGymIdAsync(Guid userId, Guid gymId)
    {
        var membership = await _repository.GetUserActiveMembershipByUserIdAndGymIdAsync(userId, gymId);
        return _mapper.Map<UserMembershipReadDto>(membership); 
    }

    public async Task<IEnumerable<UserMembershipReadDto>> GetUserPendingMembershipsByUserIdAsync(Guid userId)
    {
        var memberships = await _repository.GetUserPendingMembershipsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<UserMembershipReadDto>>(memberships); 
    }

    public async Task<Guid> CreateUserMembershipAsync(UserMembershipCreateDto dto)
    {
        var membership = await _membershipRepository.GetByIdAsync(dto.MembershipId);
        if(membership is null)
            throw new NotFoundException("Membership not found");
        
        var startDate = await _repository.GetStartDateForNewMembershipAsync(dto.UserId, membership.GymId);
        
        var canAdd = await _repository.CanAddMembershipAsync(dto.UserId, dto.MembershipId, startDate);
        if (!canAdd)
            throw new ValidationException("User already has maximum number of active/pending memberships");

        var userMembership = new UserMembership
        {
            UserId = dto.UserId,
            MembershipId = dto.MembershipId,
            StartDate = startDate,
            ExpirationDate = membership.DurationMonth.HasValue ? startDate.AddMonths((int)membership.DurationMonth) : null,
            PurchaseDate = DateTime.UtcNow,
            RemainingSessions = membership.AllowedSessions,
            Status = startDate.Date == DateTime.UtcNow.Date
                ? MembershipStatus.Active
                : MembershipStatus.Pending
        };
        
        await _repository.AddAsync(userMembership);
        
        return userMembership.Id;
    }
}