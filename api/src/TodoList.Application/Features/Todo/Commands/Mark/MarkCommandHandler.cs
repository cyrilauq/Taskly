using AutoMapper;
using FluentValidation;
using MediatR;
using TodoList.Application.Services.Exceptions;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain.Entities.Interfaces;
using TodoList.Domain.IRepository;
using UnauthorizedAccessException = TodoList.Application.Services.Exceptions.UnauthorizedAccessException;
using ValidationException = TodoList.Application.Services.Exceptions.ValidationException;

namespace TodoList.Application.Features.Todo.Commands.Mark
{
    public class MarkCommandHandler(ITodoRepository todoRepository, IUserContext userContext, IMapper mapper, IValidator<MarkCommand> validator) : IRequestHandler<MarkCommand>
    {
        public async Task Handle(MarkCommand request, CancellationToken cancellationToken)
        {
            if (!userContext.IsAuthenticated) throw new UnauthorizedAccessException("User need to be authenticated to update a todo");
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new ValidationException("One or more field aren't valid", validationResult.Errors.Select(e => e.ErrorMessage).ToArray());


            IEnumerable<Task<ITodo?>> tasks = request.TodoIds.Select(todoRepository.GetByIdAsync);
            IEnumerable<ITodo?> todos = await Task.WhenAll(tasks);

            foreach(ITodo? todo in todos)
            {
                if (todo == null) throw new ResourceNotFoundException($"One or more todo weren't found");

                if (userContext.UserId != todo.UserId && !userContext.GetRoles().Contains("Admin")) throw new UnauthorizedAccessException("You can't update the todo of another user");

                todo.IsDone = request.IsDone;
                var updatedTodo = await todoRepository.UpdateAsync(todo.Id.ToString(), todo);
            }
        }
    }
}
