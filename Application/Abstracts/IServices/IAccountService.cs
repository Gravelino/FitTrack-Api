using System.Security.Claims;
using Application.DTOs.User;
using Domain.Requests;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;

namespace Application.Abstracts.IServices;

public interface IAccountService
{
    Task RegisterAsync(RegisterRequest registerRequest);
    Task<CurrentUserDto?> LoginAsync(LoginRequest loginRequest);
    Task RefreshTokenAsync(string? refreshToken);
    Task RefreshTokenAsyncLogin(string? refreshToken);
    Task<(string, string)> RefreshTokenMobileAsync(string? refreshToken);
    Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
    Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string idToken);
    Task<CurrentUserDto> GetCurrentUserAsync(ClaimsPrincipal user);
    Task<CurrentUserDto> GetCurrentUserAsyncLogin(ClaimsPrincipal user);
    Task DeleteAsync(ClaimsPrincipal user);
    Task<(string, string, Guid?)> LoginMobileAsync(LoginMobileRequest loginRequest);
    Task DeleteByEmail(DeleteByEmailRequest deleteByEmailRequest);
    Task LogoutAsync(HttpResponse response, ClaimsPrincipal claims);
}