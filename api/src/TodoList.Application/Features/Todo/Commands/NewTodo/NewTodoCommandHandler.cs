using AutoMapper;
using MediatR;
using TodoList.Application.DTOs;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain.IRepository;
using UnauthorizedAccessException = TodoList.Application.Services.Exceptions.UnauthorizedAccessException;

namespace TodoList.Application.Features.Todo.Commands.NewTodo
{
    public class NewTodoCommandHandler(IMapper mapper, ITodoRepository todoRepository, IUserContext userContext) : IRequestHandler<NewTodoCommand, TodoDTO>
    {
        public async Task<TodoDTO> Handle(NewTodoCommand request, CancellationToken cancellationToken)
        {
            if(!userContext.IsAuthenticated) throw new UnauthorizedAccessException("User need to be authenticated to add a new todo");
            var todo = new Models.Todo
            {
                Name = request.Name,
                Content = request.Content,
                UserId = userContext.UserId
            };
            var addedTodo = await todoRepository.AddAsync(todo);
            return mapper.Map<TodoDTO>(addedTodo);
        }
    }
}
