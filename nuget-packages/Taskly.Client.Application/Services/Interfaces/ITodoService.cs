using Taskly.Client.Application.Model;

namespace Taskly.Client.Application.Services.Interfaces
{
    public interface ITodoService : ICRUDService<TodoModel, string>
    {
        Task<IEnumerable<TodoModel>> GetConnectedUserTodos();
        Task<TodoModel> SaveAsync(TodoModel model);
        Task<bool> MarkTodoAsync(Guid todoId, bool isDone);
        Task<bool> MarkMutipleTodoAsync(IEnumerable<Guid> todoIds, bool isDone);
    }
}
