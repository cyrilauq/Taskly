using TodoList.Domain.Args;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Domain.IRepository
{
    public interface ITodoRepository : ICRUDRepository<ITodo, TodoSearchArg>
    {
    }
}
