using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace RobloxDatabaseAPI.Services;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? ProvidedToken;
        string CorrectToken = Environment.GetEnvironmentVariable("APItoken") ?? "D3F4ULTT0KEN";

        if (!Request.Headers.ContainsKey("Authorization"))
        {
            Logger.Log(LogLevel.Information, "Missing Authorization Header");
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        }

        try
        {
            ProvidedToken = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
        }
        catch
        {
            Logger.Log(LogLevel.Information, "Invalid Authorization Header");
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }

        if (string.Equals(ProvidedToken, CorrectToken))
        {
            var Claims = new[] { new Claim(ClaimTypes.Name, "ROOT") };
            var Identity = new ClaimsIdentity(Claims, Scheme.Name);
            var Principal = new ClaimsPrincipal(Identity);
            var Ticket = new AuthenticationTicket(Principal, Scheme.Name);

            Logger.Log(LogLevel.Information, $"Valid token");
            return Task.FromResult(AuthenticateResult.Success(Ticket));
        }
        else
        {
            Logger.Log(LogLevel.Information, $"Invalid Token {ProvidedToken}");
            return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
        }
    }
}
