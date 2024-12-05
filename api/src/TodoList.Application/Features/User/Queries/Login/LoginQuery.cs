using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.User.Queries.Login
{
    public class LoginQuery: IRequest<UserDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
