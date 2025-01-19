using Taskly.Web.Application.Model;

namespace Taskly.Web.Application.Services.Interfaces
{
    public interface ITodoService : ICRUDService<TodoModel, string>
    {
        Task<IEnumerable<TodoModel>> GetConnectedUserTodos();
        Task<TodoModel> SaveAsync(TodoModel model);
    }
}
