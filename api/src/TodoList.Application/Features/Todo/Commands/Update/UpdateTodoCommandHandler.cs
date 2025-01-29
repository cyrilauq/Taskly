using AutoMapper;
using FluentValidation;
using MediatR;
using TodoList.Application.DTOs;
using TodoList.Application.Services.Exceptions;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain.IRepository;
using UnauthorizedAccessException = TodoList.Application.Services.Exceptions.UnauthorizedAccessException;
using ValidationException = TodoList.Application.Services.Exceptions.ValidationException;

namespace TodoList.Application.Features.Todo.Commands.Update
{
    public class UpdateTodoCommandHandler(ITodoRepository todoRepository, IUserContext userContext, IMapper mapper, IValidator<UpdateTodoCommand> validator) : IRequestHandler<UpdateTodoCommand, TodoDTO>
    {
        public async Task<TodoDTO> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            if (!userContext.IsAuthenticated) throw new UnauthorizedAccessException("User need to be authenticated to update a todo");
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new ValidationException("One or more field aren't valid", validationResult.Errors.Select(e => e.ErrorMessage).ToArray());

            var originalTodo = await todoRepository.GetByIdAsync(request.Id);

            if (originalTodo == null) throw new ResourceNotFoundException($"No todo related to the id [{request.Id}] were found");

            if (userContext.UserId != originalTodo.UserId && !userContext.GetRoles().Contains("Admin")) throw new UnauthorizedAccessException("You can't update the todo of another user");

            originalTodo.Name = request.Name;
            originalTodo.Content = request.Content;

            var updatedTodo = await todoRepository.UpdateAsync(request.Id.ToString(), originalTodo);

            return mapper.Map<TodoDTO>(updatedTodo);
        }
    }
}
