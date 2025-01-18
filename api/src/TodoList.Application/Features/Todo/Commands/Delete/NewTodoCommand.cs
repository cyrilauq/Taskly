using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todo.Commands.Delete
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
