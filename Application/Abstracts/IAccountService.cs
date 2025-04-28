using System.Security.Claims;
using Domain.Requests;

namespace Application.Abstracts;

public interface IAccountService
{
    Task RegisterAsync(RegisterRequest registerRequest);
    Task LoginAsync(LoginRequest loginRequest);
    Task RefreshTokenAsync(string? refreshToken);
    Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
}