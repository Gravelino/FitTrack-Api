using System.Security.Claims;
using Application.DTOs;
using Domain.Entities;
using Domain.Requests;
using Google.Apis.Auth;

namespace Application.Abstracts.IServices;

public interface IAccountService
{
    Task RegisterAsync(RegisterRequest registerRequest);
    Task<User?> LoginAsync(LoginRequest loginRequest);
    Task RefreshTokenAsync(string? refreshToken);
    Task<(string, string)> RefreshTokenMobileAsync(string? refreshToken);
    Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
    Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string idToken);
    Task<CurrentUserDto> GetCurrentUserAsync(ClaimsPrincipal user);
    Task DeleteAsync(ClaimsPrincipal user);
    Task<(string, string, Guid?)> LoginMobileAsync(LoginMobileRequest loginRequest);
    Task DeleteByEmail(DeleteByEmailRequest deleteByEmailRequest);
}