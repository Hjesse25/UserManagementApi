using System.Text.Json;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TokenAuthenticationMiddleware> _logger;

    // Example: Replace with your actual token validation logic
    private const string ValidToken = "TechHive-token";

    public TokenAuthenticationMiddleware(RequestDelegate next, ILogger<TokenAuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer ") || token.Substring(7) != ValidToken)
        {
            _logger.LogWarning("Unauthorized access attempt.");

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var errorResponse = new { error = "Unauthorized" };
            var json = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(json);
            return;
        }

        await _next(context); // Token is valid, continue processing
    }
}
