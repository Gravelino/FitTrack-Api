using Domain.Entities;

namespace Application.Abstracts;

public interface IUserRepository
{
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
}