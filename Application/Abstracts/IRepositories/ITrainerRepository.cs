using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface ITrainerRepository
{
    Task AddAsync(Trainer trainer);
}