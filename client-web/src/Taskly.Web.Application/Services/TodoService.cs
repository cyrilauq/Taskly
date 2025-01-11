using AutoMapper;
using Taskly.Web.Application.Exceptions;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Exceptions;
using Taskly.Web.Infrastructure.DTO;
using Taskly.Web.Infrastructure.Repositories.Interfaces;

namespace Taskly.Web.Application.Services
{
    public class TodoService(ITodoRepository todoRepository, IMapper mapper) : ITodoService
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
    }
}
