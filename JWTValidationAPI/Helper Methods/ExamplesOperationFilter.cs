using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ExamplesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Responses.ContainsKey("400"))
        {
            operation.Responses["400"].Content.Clear();
            operation.Responses["400"].Content.Add("application/json", new OpenApiMediaType
            {
                Schema = new OpenApiSchema { Type = "object", Properties = { ["message"] = new OpenApiSchema { Type = "string" } } },
                Example = new OpenApiString("{\n  \"message\": \"Error Message\"\n}")
            });
        }

        if (operation.Responses.ContainsKey("200"))
        {
            operation.Responses["200"].Content.Clear();
            operation.Responses["200"].Content.Add("application/json", new OpenApiMediaType
            {
                Schema = new OpenApiSchema { Type = "object", Properties = { ["message"] = new OpenApiSchema { Type = "string" } } },
                Example = new OpenApiString("{\n  \"valid\": true\n}")
            });
        }

        if (operation.Responses.ContainsKey("401"))
        {
            operation.Responses["401"].Content.Clear();
            operation.Responses["401"].Content.Add("application/json", new OpenApiMediaType
            {
                Schema = new OpenApiSchema { Type = "object", Properties = { ["message"] = new OpenApiSchema { Type = "string" } } },
                Example = new OpenApiString("{\n  \"message\": \"Error Message\"\n}")
            });
        }
    }
}