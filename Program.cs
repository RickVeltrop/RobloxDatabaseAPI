using Microsoft.AspNetCore.Authentication;
using NLog.Extensions.Logging;
using RobloxDatabaseAPI.Services;

namespace RobloxDatabaseAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);

            Builder.Services.AddControllers();
            Builder.Services.AddEndpointsApiExplorer();
            Builder.Services.AddSwaggerSecurityConfiguration();

            Builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();
            });

            Builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            Builder.Services.AddAuthorization(Options =>
            {
                Options.AddPolicy("RequireLoggedIn", Policy => Policy.RequireAuthenticatedUser());
            });

            var app = Builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerDocumentation();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
