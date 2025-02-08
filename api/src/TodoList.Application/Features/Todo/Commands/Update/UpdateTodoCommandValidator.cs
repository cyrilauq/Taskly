using FluentValidation;

namespace TodoList.Application.Features.Todo.Commands.Update
{
    public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
    {
        public UpdateTodoCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(25);
        }
    }
}
