using FluentValidation;

namespace TodoList.Application.Features.Todo.Commands.Mark
{
    public class MarkCommandValidator : AbstractValidator<MarkCommand>
    {
        public MarkCommandValidator() 
        {
        }
    }
}
