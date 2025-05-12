using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IMembershipRepository: IRepository<Membership>
{
    Task<IEnumerable<Membership>> GetMembershipsByGymIdAsync(Guid gymId);
}