using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace RpslsGame.GameService.Configuration;

public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public string? ValidApiKey { get; set; }
}

#pragma warning disable CS0618 // Type or member is obsolete
public class ApiKeyAuthenticationSchemeHandler : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>
{
    private readonly string _apiKeyHeader = "X-Api-Key";

    public ApiKeyAuthenticationSchemeHandler(
        IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apiKey = Context.Request.Headers[_apiKeyHeader];
        if (apiKey != Options.ValidApiKey)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Api Key"));
        }
        var claims = new[] { new Claim(ClaimTypes.Name, "VALID USER") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
#pragma warning restore CS0618 // Type or member is obsolete