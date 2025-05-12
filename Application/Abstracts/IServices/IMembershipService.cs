using Application.DTOs.Membership;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface IMembershipService: IService<MembershipReadDto, MembershipCreateDto, MembershipUpdateDto, Membership>
{
    Task<MembershipReadDto> GetMembershipByGymIdAsync(Guid gymId);
}