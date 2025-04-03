using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace FinanceService.WebApi;

public class CustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IHttpClientFactory httpClientFactory)
        : base(options, logger, encoder, clock)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return AuthenticateResult.Fail("Missing or invalid Authorization header");
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();
        var httpClient = _httpClientFactory.CreateClient("AuthService");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync("/api/v1/Authentication/IsAuthorized");

        if (!response.IsSuccessStatusCode)
        {
            return AuthenticateResult.Fail("Invalid token");
        }

        // Создаём аутентифицированного пользователя
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "AuthenticatedUser") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
