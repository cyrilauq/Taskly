using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todo.Commands.Update
{
    public class UpdateTodoCommand: IRequest<TodoDTO>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
