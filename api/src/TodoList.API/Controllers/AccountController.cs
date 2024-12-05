using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.IService;
using TodoList.Application.Features.User.Queries.Login;
using TodoList.Application.Features.User.Commands.Register;
using TodoList.Application.DTOs;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginUser(LoginQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
