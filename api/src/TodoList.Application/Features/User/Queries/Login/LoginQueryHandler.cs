using AutoMapper;
using MediatR;
using TodoList.Application.DTOs;
using TodoList.Application.IRepository;
using TodoList.Application.IService;
using TodoList.Application.Services.Exceptions;

namespace TodoList.Application.Features.User.Queries.Login
{
    internal class LoginQueryHandler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper) : IRequestHandler<LoginQuery, UserDto>
    {
        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var loggedUser = await userRepository.FindUserByCredentials(request.Login, request.Password);
            if (loggedUser == null) throw new ResourceNotFoundException("No user found for the given credentials");
            var dto = mapper.Map<UserDto>(loggedUser);
            dto.Token = await tokenService.GenerateToken(loggedUser);
            return dto;
        }
    }
}
