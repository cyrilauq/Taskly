using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
