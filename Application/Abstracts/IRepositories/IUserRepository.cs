using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    Task<User?> GetUserDetailsAsync(Guid userId, DateTime date);
}