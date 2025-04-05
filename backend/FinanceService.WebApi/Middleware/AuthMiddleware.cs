namespace FinanceService.WebApi;

public class AuthMiddleware(RequestDelegate next, HttpClient httpClient, IConfiguration configuration)
{
    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var url = configuration["AuthServiceUrl"];
        var response =
            await httpClient.PostAsJsonAsync($"{url}/api/v1/Authentication/IsAuthorized", new { Token = token });

        if (!response.IsSuccessStatusCode)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await next(context);
    }
}