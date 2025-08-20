namespace ApiVersioningDemo.Middleware;

public class DeprecationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DeprecationMiddleware> _logger;

    public DeprecationMiddleware(RequestDelegate next, ILogger<DeprecationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.Headers.ContainsKey("Deprecation"))
        {
            var endpoint = context.Request.Path;
            var userAgent = context.Request.Headers.UserAgent.ToString();
            var method = context.Request.Method;
            
            _logger.LogWarning("⚠️ DEPRECATED API ACCESSED: {Method} {Endpoint} by {UserAgent}", 
                method, endpoint, userAgent);
                
            // Also log to console for immediate visibility during demo
            Console.WriteLine($"⚠️  DEPRECATED API: {method} {endpoint}");
        }
    }
}