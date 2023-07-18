using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

public class AppAuthStateProvider : AuthenticationStateProvider
{
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // var anonymous = new ClaimsIdentity();
        // return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "John Doe"),
            new Claim(ClaimTypes.Role, "Administrator")
        };
        var anonymous = new ClaimsIdentity(claims, "testAuthType");
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
    }

    public string Teste()
    {
        return "Teste";
    }
}
