using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Taskly.Web.Application.State.Interfaces;

namespace Taskly.Web.Provider
{
    internal class AuthStateProvider : AuthenticationStateProvider
    {
        private IAuthState authState;

        public AuthStateProvider(IAuthState authState)
        {
            this.authState = authState;

            authState.OnStateChange += () => SetState();
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(SetState());
        }

        private IEnumerable<Claim> GetClaims()
        {
            return [new Claim(ClaimTypes.Name, authState.UserName)];
        }

        private AuthenticationState SetState()
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

            return state;
        }
    }
}
