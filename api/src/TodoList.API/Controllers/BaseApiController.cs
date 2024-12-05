using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController(IMediator mediator) : ControllerBase
    {
        protected IMediator Mediator { get; } = mediator;
    }
}
