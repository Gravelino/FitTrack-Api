using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IOwnerRepository
{
    Task AddAsync(Owner owner);
}