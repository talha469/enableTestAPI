using System.Reflection;
using System.Text;
using EnableBankingTest.Helper_Methods;
using EnableBankingTest.SwaggerResponses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
    .AddJsonFile("appsettings.json")
    .Build();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Enable Banking Test API",
        Version = "v1",
        Description = "API For the validation of JWT Token",
        Contact = new OpenApiContact
        {
            Name = "Muhammad Talha Arshad",
            Email = "marshad23@student.oulu.fi"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.MapType<ErrorResponse>(() => new OpenApiSchema
    {
        Type = "object",
        Properties = new Dictionary<string, OpenApiSchema>
        {
            { "message", new OpenApiSchema { Type = "string" } }
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    c.OperationFilter<ExamplesOperationFilter>();
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT Test v1");
    options.RoutePrefix = "swagger";
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
