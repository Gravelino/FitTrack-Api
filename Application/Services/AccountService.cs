using System.Security.Claims;
using Application.Abstracts;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Requests;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AccountService : IAccountService
{
    private readonly IAuthTokenProcessor _authTokenProcessor;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public AccountService(IAuthTokenProcessor authTokenProcessor, UserManager<User> userManager, IUserRepository userRepository)
    {
        _authTokenProcessor = authTokenProcessor;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task RegisterAsync(RegisterRequest registerRequest)
    {
        var userExists = await _userManager.FindByEmailAsync(registerRequest.Email) is not null;

        if (userExists)
        {
            throw new UserAlreadyExistsException(email: registerRequest.Email);
        }
        
        var user = User.Create(registerRequest.Email, registerRequest.FirstName, registerRequest.LastName);
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, registerRequest.Password);
        
        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.Select(e => e.Description));
        }
        
        await _userManager.AddToRoleAsync(user, GetIdentityRoleName(registerRequest.Role));
    }

    public async Task LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            throw new LoginFailedException(loginRequest.Email);
        }
        
        var roles = await _userManager.GetRolesAsync(user);

        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user, roles);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();
        
        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);
        
        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;
        
        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }

    public async Task RefreshTokenAsync(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new RefreshTokenException("Refresh token is missing.");
        }
        
        var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

        if (user is null)
        {
            throw new RefreshTokenException("Unable to retrieve user for refresh token.");
        }

        if (user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
        {
            throw new RefreshTokenException("Refresh token is expired.");
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user, roles);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();
        
        var refreshTokenExpirationDateInUtc = expirationDateInUtc.AddDays(7);
        
        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;
        
        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }

    private string GetIdentityRoleName(Role role)
    {
        return role switch
        {
            Role.User => IdentityRoleConstants.User,
            Role.Owner => IdentityRoleConstants.Owner,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, "Provided role is not supported.")
        };
    }
    
    public async Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal)
    {
        if (claimsPrincipal is null)
        {
            throw new ExternalLoginProviderException("Google", "ClaimsPrincipal is null");
        }
        
        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        if (email is null)
        {
            throw new ExternalLoginProviderException("Google", "Email is null");
        }
        
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            var newUser = new User
            {
                UserName = email,
                Email = email,
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                PictureUrl = claimsPrincipal.FindFirstValue("picture") ?? string.Empty,
                EmailConfirmed = true,
            };
            
            var result = await _userManager.CreateAsync(newUser);

            if (!result.Succeeded)
            {
                throw new ExternalLoginProviderException("Google",
                    $"Unable to create new user: {string.Join(", ",
                        result.Errors.Select(e => e.Description))}");
            }

            user = newUser;
        }
        
        var info = new UserLoginInfo("Google",
            claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            "Google");

        var userLogins = await _userManager.GetLoginsAsync(user);
        var hasGoogleLogin = userLogins.Any(x => x.LoginProvider == 
                                                 info.LoginProvider && x.ProviderKey == info.ProviderKey);

        if (!hasGoogleLogin)
        {
            var loginResult = await _userManager.AddLoginAsync(user, info);

            if (!loginResult.Succeeded)
            {
                throw new ExternalLoginProviderException("Google",
                    $"Unable to login the user: {string.Join(", ",
                        loginResult.Errors.Select(e => e.Description))}");
            }
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user, roles);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();
        
        var refreshTokenExpirationDateInUtc = expirationDateInUtc.AddDays(7);
        
        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;
        
        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }
}