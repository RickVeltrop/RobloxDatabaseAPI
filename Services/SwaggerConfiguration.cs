using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RobloxDatabaseAPI.Services;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerSecurityConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Main API v1.0", Version = "v1.0" });

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");
            config.DocExpansion(DocExpansion.None);

            config.OAuthClientId("swagger");
        });

        return app;
    }
}
