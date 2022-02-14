using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InventoryAPI.WebAPI.Filters;

public class CustomAuthenticationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.DeclaringType != null &&
            (context.MethodInfo.GetCustomAttributes(true)
                 .Any(x => x is AuthorizeAttribute) || 
             context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                 .Any(x => x is AuthorizeAttribute)))
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
        }
    }
}