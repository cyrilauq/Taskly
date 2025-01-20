using System.Net.Http.Json;
using Taskly.Client.Domain.DTO;
using Taskly.Client.Domain.Repositories.Interfaces;
using Taskly.Client.Infrastructure.Repository.Utils;

namespace Taskly.Client.Infrastructure.Repository
{
    public class TodoApiRepository(HttpClient httpClient) : ITodoRepository
    {
        public async Task<TodoDTO> Create(TodoDTO entity)
        {
            var response = await httpClient.PostAsJsonAsync("api/todo", entity);
            return (await response.Content.ToJson<TodoDTO>())!;
        }

        public async Task<bool> Delete(Guid key)
        {
            var response = await httpClient.DeleteAsync($"api/todo/{key}");
            return true;
        }

        public async Task<IEnumerable<TodoDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodoDTO>> GetAllForUser(Guid userId)
        {
            var response = await httpClient.GetAsync($"api/todo?userId={userId}");
            return await response.Content.ToJson<IEnumerable<TodoDTO>>() ?? [];
        }

        public async Task<TodoDTO> Update(Guid key, TodoDTO entity)
        {
            var response = await httpClient.PutAsJsonAsync($"api/todo/{key}", entity);
            return (await response.Content.ToJson<TodoDTO>())!;
        }
    }
}
