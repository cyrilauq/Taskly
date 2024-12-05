using TodoList.Application.DTOs;

namespace TodoList.Application.IService;

public interface IAuthenticationService
{
    Task<UserDto> Login(LoginDto loginDto);
    Task<UserDto> Register(RegisterDto registerDto);
}
