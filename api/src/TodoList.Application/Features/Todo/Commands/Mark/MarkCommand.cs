using MediatR;
namespace TodoList.Application.Features.Todo.Commands.Mark
{
    public class MarkCommand: IRequest
    {
        public Guid[] TodoIds { get; set; } = [];
        public bool IsDone { get; set; }
    }
}
