﻿using TodoList.Application.Args;
using TodoList.Application.DTOs;
using TodoList.Application.IRepository;
using TodoList.Application.IService;
using TodoList.Domain.Entities;
using TodoList.Application.Services.Exceptions;

namespace TodoList.Application.Services
{
    public class TodoService(ICRUDRepository<Todo, TodoSearchArg> todoRepository) : ITodoService
    {
        public Task<bool> Delete(string Id, TodoDTO todo)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoDTO> Save(TodoDTO todo)
        {
            return todo.Id == null ? await AddTodo(todo) : await Save(todo);
        }

        private async Task<TodoDTO> AddTodo(TodoDTO todoDTO)
        {
            var validationsErrors = ValidateTodo(todoDTO);
            if (validationsErrors.Any()) throw new ValidationException("The given todo isn't valid", validationsErrors);
            return ToDTO(await todoRepository.AddAsync(ToEntity(todoDTO)));
        }

        private async Task<TodoDTO> UpdateTodo(TodoDTO todoDTO)
        {
            var validationsErrors = ValidateTodo(todoDTO);
            if (validationsErrors.Any()) throw new ValidationException("The given todo isn't valid", validationsErrors);
            return ToDTO(await todoRepository.UpdateAsync(todoDTO.Id!, ToEntity(todoDTO)));
        }

        private TodoDTO ToDTO(Todo todo)
        {
            // TODO
            throw new NotImplementedException();
        }

        private string[] ValidateTodo(TodoDTO todoDTO)
        {
            IList<string> errors = new List<string>();
            if (todoDTO.Name == null || todoDTO.Name.Length == 0) errors.Add("The todo name is required");
            return errors.ToArray();
        }

        private Todo ToEntity(TodoDTO todoDTO)
        {
            var now = DateTime.UtcNow;
            return new()
            {
                IsDone = todoDTO.IsDone,
                Id = Guid.Parse(todoDTO.Id!),
                CreatedOn = todoDTO.CreatedOn ?? now,
                Content = todoDTO.Content,
                Name = todoDTO.Name!,
                UpdatedOn = now
            };
        }
    }
}
