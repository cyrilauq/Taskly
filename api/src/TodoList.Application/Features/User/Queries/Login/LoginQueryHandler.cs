using MediatR;
using TodoList.Application.DTOs;
using TodoList.Application.IRepository;
using TodoList.Application.IService;
using TodoList.Application.Services.Exceptions;

namespace TodoList.Application.Features.User.Queries.Login
{
    internal class LoginQueryHandler(IUserRepository userRepository, ITokenService tokenService) : IRequestHandler<LoginQuery, UserDto>
    {
        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var loggedUser = await userRepository.FindUserByCredentials(request.Login, request.Password);
            if (loggedUser == null) throw new ResourceNotFoundException("No user found for the given credentials");
            return new UserDto
            {
                BirthDate = loggedUser.BirthDate,
                Pseudo = loggedUser.UserName,
                Lastname = loggedUser.Lastname,
                Firstname = loggedUser.Firstname,
                Id = loggedUser.Id.ToString(),
                Token = tokenService.GenerateToken(loggedUser)
            };
        }
    }
}
