using System.Net.Http.Json;
using System.Text.Json;
using Taskly.Client.Domain.DTO;
using Taskly.Client.Domain.Repositories.Interfaces;

namespace Taskly.Client.Infrastructure.Repository
{
    public class UserApiRepository(HttpClient httpClient) : IUserRepository
    {
        public async Task<UserDTO> Add(RegisterArgs args)
        {
            using var response = await httpClient.PostAsJsonAsync("api/account/login", args);
            using Stream responseContent = await response.Content.ReadAsStreamAsync()!;
            return (await JsonSerializer.DeserializeAsync<UserDTO>(responseContent))!;
        }

        public async Task<UserDTO> GetByCredentials(LoginArgs args)
        {
            using var response = await httpClient.PostAsJsonAsync("api/account/login", args);
            using Stream responseContent = await response.Content.ReadAsStreamAsync()!;
            return (await JsonSerializer.DeserializeAsync<UserDTO>(responseContent))!;
        }
    }
}
