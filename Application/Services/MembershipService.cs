using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Membership;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class MembershipService: Service<MembershipReadDto, MembershipCreateDto, MembershipUpdateDto, Membership>,
    IMembershipService
{
    private readonly IMembershipRepository _repository;

    public MembershipService(IMembershipRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<MembershipReadDto> GetMembershipByGymIdAsync(Guid gymId)
    {
        var memberships = await _repository.GetMembershipsByGymIdAsync(gymId);
        return _mapper.Map<MembershipReadDto>(memberships);
    }
}