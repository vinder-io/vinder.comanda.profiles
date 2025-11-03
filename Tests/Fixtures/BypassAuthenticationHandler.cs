using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace Vinder.Comanda.Profiles.TestSuite.Fixtures;

public sealed class BypassAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) :
    AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "vinder.internal.bypass.user"),
            new Claim(ClaimTypes.NameIdentifier, "vinder.internal.bypass.user"),

            new Claim(ClaimTypes.Role, Permissions.ViewActivities),
            new Claim(ClaimTypes.Role, Permissions.ViewOwners),
            new Claim(ClaimTypes.Role, Permissions.CreateOwners),
            new Claim(ClaimTypes.Role, Permissions.EditOwners),
            new Claim(ClaimTypes.Role, Permissions.DeleteOwners),

            new Claim(ClaimTypes.Role, Permissions.ViewCustomers),
            new Claim(ClaimTypes.Role, Permissions.CreateCustomers),
            new Claim(ClaimTypes.Role, Permissions.EditCustomers),
            new Claim(ClaimTypes.Role, Permissions.DeleteCustomers),
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}