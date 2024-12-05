using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.User.Commands.Register
{
    public class RegisterCommand: IRequest<UserDto>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
