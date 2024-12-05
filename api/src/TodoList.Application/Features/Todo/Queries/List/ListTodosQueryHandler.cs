using MediatR;
using TodoList.Application.Args;
using TodoList.Application.DTOs;
using TodoList.Application.IRepository;

namespace TodoList.Application.Features.Todo.Queries.List
{
    public class ListTodosQueryHandler(ICRUDRepository<Domain.Entities.Todo, TodoSearchArg> todoRepository) : IRequestHandler<ListTodosQuery, List<TodoDTO>>
    {
        public async Task<List<TodoDTO>> Handle(ListTodosQuery request, CancellationToken cancellationToken)
        {
            return (await todoRepository.GetAllAsync(new TodoSearchArg { UserId = request.UserId, IsDeleted = request.IsDeleted }))
                .Select(t => new TodoDTO(t.Id.ToString(), t.Content ?? "No content", t.Name, t.UserId, t.IsDone, t.CreatedOn, t.DeletedOn))
                .ToList();
        }
    }
}
