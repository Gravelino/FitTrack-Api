using System.Security.Claims;
using Application.Abstracts;
using Domain.Entities;
using Domain.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        catch (Google.Apis.Auth.InvalidJwtException)
        {
            return BadRequest("Invalid Google token");
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
        return Ok();
    }
}