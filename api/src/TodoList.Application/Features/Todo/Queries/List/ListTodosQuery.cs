using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todo.Queries.List
{
    public record ListTodosQuery: IRequest<List<TodoDTO>>
    {
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
    }
}
