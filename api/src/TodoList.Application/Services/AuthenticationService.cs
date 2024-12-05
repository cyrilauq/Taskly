using TodoList.Application.DTOs;
using TodoList.Application.IRepository;
using TodoList.Application.IService;
using TodoList.Domain.Entities;
using TodoList.Application.Services.Exceptions;
using TodoList.Domain.Entities.Interfaces;
using TodoList.Application.Models;

namespace TodoList.Application.Services;

public class AuthenticationService(IUserRepository userRepository, ITokenService tokenService) : IAuthenticationService
{
    public async Task<UserDto> Login(LoginDto loginDto)
    {
        var loggedUser = await userRepository.FindUserByCredentials(loginDto.UserName, loginDto.Password);
        if (loggedUser == null) throw new ResourceNotFoundException("No user found for the given credentials");
        return ToDto(loggedUser);
    }

    public async Task<UserDto> Register(RegisterDto registerDto)
    {
        try
        {
            if (await userRepository.FindUserByEmailOrUsername(registerDto.Email, registerDto.Pseudo) != null)
            {
                throw new ResourceAlreadyExists("The given username or email is already used");
            }
            var user = new User
            {
                Email = registerDto.Email,
                Firstname = registerDto.Firstname,
                Lastname = registerDto.Lastname,
                UserName = registerDto.Pseudo,
                BirthDate = registerDto.BirthDate
            };
            var addedUser = await userRepository.Add(user, registerDto.Password);
            return ToDto(addedUser);
        }
        catch
        {
            throw;
        }
    }

    private UserDto ToDto(IUser user)
    {
        return new UserDto
        {
            BirthDate = user.BirthDate,
            Pseudo = user.UserName,
            Lastname = user.Lastname,
            Firstname = user.Firstname,
            Id = user.Id.ToString(),
            Token = tokenService.GenerateToken(user)
        };
    }
}