using Taskly.Client.Domain.DTO;

namespace Taskly.Client.Domain.Repositories.Interfaces
{
    public interface ITodoRepository : ICRUDRepository<TodoDTO, Guid>
    {
        Task<IEnumerable<TodoDTO>> GetAllForUser(Guid userId);

        Task<bool> MarkTodos(IEnumerable<Guid> todoIds, bool isDone);
    }
}
