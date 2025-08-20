using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiVersioningDemo.Extensions;

public class DeprecationDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var path in swaggerDoc.Paths.Values)
        {
            foreach (var operation in path.Operations.Values)
            {
                if (operation.Tags?.Any(tag => tag.Name.Contains("v1.0")) == true)
                {
                    operation.Summary = "⚠️ DEPRECATED - " + operation.Summary;
                    operation.Description = "🚨 **This endpoint is deprecated and will be removed on 2024-12-31**\n\n" +
                                          "Please migrate to the v2.0 equivalent.\n\n" + 
                                          operation.Description;
                }
            }
        }
    }
}