using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiVersioningDemo.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class DeprecatedAttribute : ActionFilterAttribute
{
    public string? DeprecationDate { get; set; }
    public string? SunsetDate { get; set; }
    public string? AlternativeVersion { get; set; }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var response = context.HttpContext.Response;
        
        response.Headers.Add("Deprecation", "true");
        
        if (!string.IsNullOrEmpty(DeprecationDate))
            response.Headers.Add("Deprecation-Date", DeprecationDate);
            
        if (!string.IsNullOrEmpty(SunsetDate))
            response.Headers.Add("Sunset", SunsetDate);
            
        if (!string.IsNullOrEmpty(AlternativeVersion))
            response.Headers.Add("Link", $"</api/v{AlternativeVersion}/products>; rel=\"successor-version\"");

        base.OnActionExecuted(context);
    }
}