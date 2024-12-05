using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Features.Todo.Queries.List;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListTodos()
        {
            return Ok(await mediator.Send(new ListTodosQuery()));
        }
    }
}
