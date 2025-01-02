using System.Net.Http.Json;
using TodoList.Web.Application.Services.Interfaces;

namespace TodoList.Web.Application.Services
{
    internal class AuthenticationService(HttpClient httpClient) : IAuthenticationService
    {

        public async Task<bool> LoginWithCredentials(string login, string password)
        {
            using var response = await httpClient.PostAsJsonAsync("api/account/login", new { Login = login, Password = password });

            return true;
        }
    }
}
