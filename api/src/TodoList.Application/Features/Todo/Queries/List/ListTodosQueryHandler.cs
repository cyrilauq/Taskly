﻿using MediatR;
using Microsoft.AspNetCore.Http;
using TodoList.Application.Args;
using TodoList.Application.DTOs;
using TodoList.Application.Extensions;
using TodoList.Application.IRepository;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Features.Todo.Queries.List
{
    public class ListTodosQueryHandler(ICRUDRepository<ITodo, TodoSearchArg> todoRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<ListTodosQuery, List<TodoDTO>>
    {
        public async Task<List<TodoDTO>> Handle(ListTodosQuery request, CancellationToken cancellationToken)
        {
            var connectedUserId = Guid.Parse(httpContextAccessor.ConnectedUserId()!);
            if (!UserCanListTodo(connectedUserId, request.UserId, httpContextAccessor.ConnectedUserRoles())) throw new UnauthorizedAccessException("You can't see the request resource");
            return (await todoRepository.GetAllAsync(new TodoSearchArg { UserId = connectedUserId, IsDeleted = request.IsDeleted }))
                .Select(t => new TodoDTO(t.Id.ToString(), t.Content ?? "No content", t.Name, t.UserId, t.IsDone, t.CreatedOn, t.DeletedOn))
                .ToList();
        }

        private bool UserCanListTodo(Guid connectedUserId, Guid requestUserId, IEnumerable<string> userRoles)
        {
            if(userRoles.Contains("Admin"))
            {
                return true;
            }
            return requestUserId == Guid.Empty || requestUserId == connectedUserId;
        }
    }
}
