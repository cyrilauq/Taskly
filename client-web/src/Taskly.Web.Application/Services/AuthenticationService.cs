using System.Net.Http.Json;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Application.State.Interfaces;

namespace Taskly.Web.Application.Services
{
    internal class AuthenticationService(HttpClient httpClient, IAuthState authState) : IAuthenticationService
    {

        public async Task<bool> LoginWithCredentials(string login, string password)
        {
            using var response = await httpClient.PostAsJsonAsync("api/account/login", new { Login = login, Password = password });
            AuthenticatedUserModel user = (await response.Content.ReadFromJsonAsync<AuthenticatedUserModel>())!;

            authState.Token = user.Token;
            authState.UserName = user.Pseudo;
            authState.UserId = user.Id;
            authState.NotifyStateChanged();

            return true;
        }

        public async Task<bool> RegisterUser(RegisterModel registerModel)
        {
            using var response = await httpClient.PostAsJsonAsync("api/account/register", registerModel);
            AuthenticatedUserModel user = (await response.Content.ReadFromJsonAsync<AuthenticatedUserModel>())!;

            authState.Token = user.Token;
            authState.UserName = user.Pseudo;
            authState.UserId = user.Id;
            authState.NotifyStateChanged();

            return false;
        }
    }
}
