using Domain.Entities;

namespace Application.Abstracts;

public interface IAuthTokenProcessor
{
    (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user, IList<string> roles);
    (string jwtToken, DateTime expiresAtUtc) GenerateJwtTokenByLogin(User user, IList<string> roles);
    string GenerateRefreshToken();

    void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token,
        DateTime expiration);
}