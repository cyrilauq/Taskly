using MediatR;
using TodoList.Application.Services.Exceptions;
using TodoList.Domain.Exceptions;
using TodoList.Domain.IRepository;

namespace TodoList.Application.Features.Todo.Commands.Delete
{
    public class DeleteTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, bool>
    {
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await todoRepository.DeleteAsync(request.Id, cancellationToken);
            }
            catch (EntityNotExistsException)
            {
                throw new ResourceNotFoundException("");
            }
        }
    }
}
