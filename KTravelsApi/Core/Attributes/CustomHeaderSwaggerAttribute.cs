using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KTravelsApi.Core.Attributes;

/// <summary>
/// Add custom header parameters to swagger (OpenAPI) documentation.
/// </summary>
public class CustomHeaderSwaggerAttribute : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Bearer",
            Description = "A JWT access token",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string<JwtToken>"
            }
        });
    }
}