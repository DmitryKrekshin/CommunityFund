using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.WebApi;

[ApiController]
[AllowAnonymous]
[Route("api/v1/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private const string UnhandledExceptionMessage = "Unhandled exception";

    private readonly IAuthenticationService _authenticationService;

    private readonly IConfiguration _configuration;

    private readonly TokenValidationParameters _tokenValidationParameters;

    public AuthenticationController(IAuthenticationService authenticationService, IConfiguration configuration)
    {
        _authenticationService = authenticationService;
        _configuration = configuration;

        var secretKey = configuration["SymmetricSecurityKey"];
        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "auth-service",
            ValidateAudience = true,
            ValidAudience = "auth-service",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
    }

    [HttpPost]
    public async Task<IActionResult> Authorize([FromBody] AuthorizeRequest authorizeRequest,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _authenticationService.AuthenticateAsync(authorizeRequest.Login, authorizeRequest.Password,
                cancellationToken);

            return user != null ? Ok(new { Token = GenerateJwtToken(user) }) : Unauthorized();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    public IActionResult IsAuthorized([FromBody] IsAuthorizedRequest isAuthorizedRequest,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(isAuthorizedRequest.Token, _tokenValidationParameters, out _);
            return Ok(new { IsValid = true, Claims = principal.Claims.Select(c => new { c.Type, c.Value }) });
        }
        catch
        {
            return Unauthorized(new { IsValid = false });
        }
    }

    private string GenerateJwtToken(UserEntity user)
    {
        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "auth-service",
            audience: "auth-service",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(14),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}