﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Features.Todo.Commands.Delete;
using TodoList.Application.Features.Todo.Commands.Mark;
using TodoList.Application.Features.Todo.Commands.NewTodo;
using TodoList.Application.Features.Todo.Commands.Update;
using TodoList.Application.Features.Todo.Queries.List;

namespace TodoList.API.Controllers
{
    [Authorize]
    public class TodoController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> ListTodos([FromQuery] ListTodosQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command" example="new NewTodoCommand()"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> NewTodo(NewTodoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{todoId}")]
        public async Task<IActionResult> DeleteTodo(Guid todoId)
        {
            await Mediator.Send(new DeleteTodoCommand() { Id = todoId });
            return NoContent();
        }

        [HttpPut("{todoId}")]
        public async Task<IActionResult> UpdateTodo(Guid todoId, UpdateTodoCommand command)
        {
            command.Id = todoId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("mark")]
        public async Task<IActionResult> MarkTodoAsRead(MarkCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
