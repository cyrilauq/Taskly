using System.Net.Http.Json;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Application.State.Interfaces;

namespace Taskly.Web.Application.Services
{
    internal class AuthenticationService(HttpClient httpClient, IAuthState authState) : IAuthenticationService
    {

        public async Task<bool> LoginWithCredentials(string login, string password)
        {
            using var response = await httpClient.PostAsJsonAsync("api/account/login", new { Login = login, Password = password });

            authState.UserName = login;
            authState.UserId = Guid.NewGuid();

            return true;
        }
    }
}
