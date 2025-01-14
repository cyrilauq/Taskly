using AutoMapper;
using Taskly.Web.Application.Exceptions;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Application.State.Interfaces;
using Taskly.Web.Exceptions;
using Taskly.Web.Infrastructure.DTO;
using Taskly.Web.Infrastructure.Repositories.Interfaces;
using UnauthorizedAccessException = Taskly.Web.Application.Exceptions.UnauthorizedAccessException;

namespace Taskly.Web.Application.Services
{
    public class TodoService(ITodoRepository todoRepository, IMapper mapper, IAuthState authState) : ITodoService
    {
        public async Task<TodoModel> CreateAsync(TodoModel entity)
        {
            try
            {
                var dto = mapper.Map<TodoDTO>(entity);
                return mapper.Map<TodoModel>(await todoRepository.Create(dto));
            }
            catch (ValidationException ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public Task<bool> DeleteAsync(string entityKey, CancellationToken cancellationToken = default)
        {
            try
            {
                var guid = Guid.Empty;
                if(Guid.TryParse(entityKey, out guid))
                {
                    throw new ArgumentException("The provided id isn't a valid guid");
                }
                return todoRepository.Delete(guid);
            }
            catch (NotFoundException ex)
            {
                throw new ServiceException($"No todo found for the id [{entityKey}]");
            }
        }

        public async Task<IEnumerable<TodoModel>> GetConnectedUserTodos()
        {
            if (authState.UserId is null) throw new UnauthorizedAccessException("You need to be logged in to access the resource");
            return mapper.Map<IEnumerable<TodoModel>>(await todoRepository.GetAllForUser(authState.UserId.Value));
        }
    }
}
