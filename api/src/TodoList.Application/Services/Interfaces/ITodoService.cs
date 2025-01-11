using TodoList.Application.DTOs;

namespace TodoList.Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<TodoDTO> Save(TodoDTO todo);
        Task<bool> Delete(string Id, TodoDTO todo);
    }
}
