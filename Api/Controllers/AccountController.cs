using System.Security.Claims;
using Application.Abstracts;
using Domain.Entities;
using Domain.Requests;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RefreshTokenRequest = Domain.Requests.RefreshTokenRequest;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly LinkGenerator _linkGenerator;
    private readonly IAccountService _accountService;

    public AccountController(SignInManager<User> signInManager,
        LinkGenerator linkGenerator,
        IAccountService accountService)
    {
        _signInManager = signInManager;
        _linkGenerator = linkGenerator;
        _accountService = accountService;
    }

    [HttpGet("login/google")]
    public Task<IActionResult> LoginWithGoogleAsync([FromQuery] string returnUrl = "/")
    {
        var callbackUrl = _linkGenerator.GetPathByAction(HttpContext, nameof(GoogleLoginCallback), "Account") 
                          + $"?returnUrl={returnUrl}";      
        
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", callbackUrl);

        return Task.FromResult<IActionResult>(Challenge(properties, GoogleDefaults.AuthenticationScheme));
    }
    
    [HttpGet("login/google/callback", Name = "GoogleLoginCallback")]
    public async Task<IActionResult> GoogleLoginCallback([FromQuery] string returnUrl = "/")
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        await _accountService.LoginWithGoogleAsync(result.Principal);

        return Redirect(returnUrl);
    }

    [HttpPost("login/google-token")]
    public async Task<IActionResult> LoginWithGoogleToken([FromBody] GoogleLoginRequest request)
    {
        try
        {
            var payload = await _accountService.ValidateGoogleToken(request.IdToken);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, payload.Subject),
                new Claim(ClaimTypes.Email, payload.Email),
                new Claim(ClaimTypes.GivenName, payload.GivenName ?? ""),
                new Claim(ClaimTypes.Surname, payload.FamilyName ?? ""),
                new Claim("picture", payload.Picture ?? "")
            };

            var identity = new ClaimsIdentity(claims, "Google");
            var principal = new ClaimsPrincipal(identity);

            await _accountService.LoginWithGoogleAsync(principal);

            return Ok();
        }
        catch (Google.Apis.Auth.InvalidJwtException ex)
        {
            return BadRequest($"Invalid Google token: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _accountService.LoginAsync(loginRequest);

        if (user is null)
        {
            return Unauthorized();
        }

        return Ok(user);
    }
    
    [HttpPost("login-mobile")]
    public async Task<IActionResult> LoginMobile([FromBody] LoginMobileRequest loginRequest)
    {
        var user = await _accountService.LoginMobileAsync(loginRequest);

        if (user.Item3 is null)
        {
            return Unauthorized();
        }

        return Ok(new{ User = user.Item3, AccessToken = user.Item1, RefreshToken = user.Item2 });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["REFRESH_TOKEN"];
        await _accountService.RefreshTokenAsync(refreshToken);
        return Ok();
    }

    [HttpPost("refresh-token/mobile")]
    public async Task<IActionResult> RefreshTokenMobile([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        var tokens = await _accountService.RefreshTokenMobileAsync(refreshTokenRequest.RefreshToken);
        return Ok(new { AccessToken = tokens.Item1, RefreshToken = tokens.Item2 });
    }
    
    [HttpGet("me")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await _accountService.GetCurrentUserAsync(User);
        return Ok(user);    
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteCurrentUser()
    {
        await _accountService.DeleteAsync(User);
        await _signInManager.SignOutAsync();
        return NoContent();
    }
    
    [HttpDelete("by-email")]
    [Authorize]
    public async Task<IActionResult> DeleteUserByEmail([FromBody] DeleteByEmailRequest deleteByEmailRequest)
    {
        await _accountService.DeleteByEmail(deleteByEmailRequest);
        await _signInManager.SignOutAsync();
        return NoContent();
    }
}