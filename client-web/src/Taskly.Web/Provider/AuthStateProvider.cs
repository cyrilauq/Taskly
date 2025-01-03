using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Taskly.Web.Application.State.Interfaces;

namespace Taskly.Web.Provider
{
    internal class AuthStateProvider(IAuthState authState) : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            if (authState.IsAuthenticated)
            {
                identity = new ClaimsIdentity(GetClaims(), "JWT Authentication");
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            // If not done, then state isn't aware of its changes
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return Task.FromResult(state);
        }

        private IEnumerable<Claim> GetClaims()
        {
            return [new Claim(ClaimTypes.Name, authState.UserName)];
        }
    }
}
