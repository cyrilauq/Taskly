using TodoList.Application.Args;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.IRepository
{
    public interface ITodoRepository: ICRUDRepository<ITodo, TodoSearchArg>
    {
    }
}
