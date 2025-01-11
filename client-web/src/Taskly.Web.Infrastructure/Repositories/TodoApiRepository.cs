using System.Net.Http.Json;
using Taskly.Web.Infrastructure.DTO;
using Taskly.Web.Infrastructure.Repositories.Interfaces;
using Taskly.Web.Infrastructure.Utils;

namespace Taskly.Web.Infrastructure.Repositories
{
    public class TodoApiRepository(HttpClient httpClient) : ITodoRepository
    {
        public async Task<TodoDTO> Create(TodoDTO entity)
        {
            using var response = await httpClient.PostAsJsonAsync("api/todo", entity);
            return (await response.Content.ToJson<TodoDTO>())!;
        }

        public Task<bool> Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TodoDTO> Update(Guid key, TodoDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
