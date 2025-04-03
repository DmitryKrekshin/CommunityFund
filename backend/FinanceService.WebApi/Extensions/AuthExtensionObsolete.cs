using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FinanceService.WebApi;

public static class AuthExtensionObsolete
{
    // todo не работает, пока использую кастомный AuthMiddleware
    public static AuthenticationBuilder AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authServiceUrl = configuration["AuthServiceUrl"];
        if (string.IsNullOrEmpty(authServiceUrl))
        {
            throw new ArgumentNullException("AuthServiceUrl", "Auth service URL is not configured.");
        }

        services.AddHttpClient("AuthService", client =>
        {
            client.BaseAddress = new Uri(authServiceUrl);
        });

        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].ToString();
                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                        {
                            context.Token = authHeader.Substring("Bearer ".Length).Trim();
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var token = context.SecurityToken as JwtSecurityToken;
                        if (token == null)
                        {
                            context.Fail("Invalid token");
                            return;
                        }

                        var httpClientFactory = context.HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>();
                        var httpClient = httpClientFactory.CreateClient("AuthService");

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.RawData);
                        var response = await httpClient.GetAsync("/api/v1/Authentication/IsAuthorized");

                        if (!response.IsSuccessStatusCode)
                        {
                            context.Fail("Unauthorized");
                        }
                    }
                };
            });
    }
}