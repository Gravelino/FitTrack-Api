using Domain.Entities;

namespace Application.Abstracts;

public interface ITrainerRepository
{
    Task AddAsync(Trainer trainer);
}