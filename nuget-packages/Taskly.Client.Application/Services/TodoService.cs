﻿using AutoMapper;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Client.Domain.DTO;
using Taskly.Client.Domain.Repositories.Interfaces;
using UnauthorizedAccessException = Taskly.Client.Application.Exceptions.UnauthorizedAccessException;
using ValidationException = Taskly.Client.Application.Exceptions.ValidationException;

namespace Taskly.Client.Application.Services
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

        public async Task<bool> DeleteAsync(string entityKey, CancellationToken cancellationToken = default)
        {
            try
            {
                var guid = Guid.Empty;
                if (Guid.TryParse(entityKey, out guid))
                {
                    return await todoRepository.Delete(guid);
                }
                throw new ServiceException("The provided id isn't a valid guid");
            }
            catch (NotFoundException)
            {
                throw new ServiceException($"No todo found for the id [{entityKey}]");
            }
        }

        public async Task<IEnumerable<TodoModel>> GetConnectedUserTodos()
        {
            if (authState.UserId is null) throw new UnauthorizedAccessException("You need to be logged in to access the resource");
            return mapper.Map<IEnumerable<TodoModel>>(await todoRepository.GetAllForUser(authState.UserId.Value));
        }

        public async Task<TodoModel> SaveAsync(TodoModel model)
        {
            Guid parseResult;
            return model.Id == Guid.Empty.ToString() || !Guid.TryParse(model.Id, out parseResult) ? await CreateAsync(model) : await UpdateAsync(model.Id, model);
        }

        public async Task<TodoModel> UpdateAsync(string key, TodoModel updatedEntity)
        {
            try
            {
                var guid = Guid.Empty;
                if (Guid.TryParse(key, out guid))
                {
                    var dto = mapper.Map<TodoDTO>(updatedEntity);
                    return mapper.Map<TodoModel>(await todoRepository.Update(guid, dto));
                }
                throw new ServiceException("The provided id isn't a valid guid");
            }
            catch (ValidationException ex)
            {
                throw new ServiceException(ex.Message);
            }
            catch (NotFoundException)
            {
                throw new ServiceException($"No todo found for the id [{key}]");
            }
            catch (UnauthorizedAccessException)
            {
                throw new ServiceException($"You don't have the rights to update the todo.");
            }
        }

        public async Task<bool> MarkTodoAsync(Guid todoId, bool isDone)
        {
            try
            {
                return await todoRepository.MarkTodos([ todoId ], isDone);
            }
            catch (ValidationException ex)
            {
                throw new ServiceException(ex.Message);
            }
            catch (NotFoundException)
            {
                throw new ServiceException($"No todo found for the id [{todoId}]");
            }
            catch (UnauthorizedAccessException)
            {
                throw new ServiceException($"You don't have the rights to update the todo.");
            }
        }

        public async Task<bool> MarkMutipleTodoAsync(IEnumerable<Guid> todoIds, bool isDone)
        {
            try
            {
                return await todoRepository.MarkTodos(todoIds, isDone);
            }
            catch (ValidationException ex)
            {
                throw new ServiceException(ex.Message);
            }
            catch (NotFoundException)
            {
                throw new ServiceException($"One or more todos weren't found");
            }
            catch (UnauthorizedAccessException)
            {
                throw new ServiceException($"You don't have the rights to update the todo.");
            }
        }
    }
}
