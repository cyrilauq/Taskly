using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todo.Commands.NewTodo
{
    public class NewTodoCommand : IRequest<TodoDTO>
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }
}
