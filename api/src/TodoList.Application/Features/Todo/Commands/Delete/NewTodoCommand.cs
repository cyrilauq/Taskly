using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todo.Commands.NewTodo
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
