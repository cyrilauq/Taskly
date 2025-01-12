using Taskly.Web.Application.Model;

namespace Taskly.Web.Application.Services.Interfaces
{
    public interface ITodoService : ICRUDService<TodoModel>
    {
        Task<IEnumerable<TodoModel>> GetConnectedUserTodos();
    }
}
