using Microsoft.AspNetCore.Authentication;

namespace FinanceService.WebApi;

public static class AuthExtension
{
    public static AuthenticationBuilder AddCustomAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient("AuthService", client =>
        {
            client.BaseAddress = new Uri(configuration["AuthServiceUrl"]); // URL AuthService
        });

        return services.AddAuthentication("CustomAuthScheme")
            .AddScheme<AuthenticationSchemeOptions, CustomAuthHandler>("CustomAuthScheme", null);
    }
}