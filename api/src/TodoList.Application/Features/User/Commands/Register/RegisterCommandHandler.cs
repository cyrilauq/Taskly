using MediatR;
using TodoList.Application.DTOs;
using TodoList.Application.IRepository;
using TodoList.Application.IService;
using TodoList.Application.Services.Exceptions;

namespace TodoList.Application.Features.User.Commands.Register
{
    public class RegisterCommandHandler(IUserRepository userRepository, ITokenService tokenService) : IRequestHandler<RegisterCommand, UserDto>
    {
        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await userRepository.FindUserByEmailOrUsername(request.Email, request.Pseudo) != null)
            {
                throw new ResourceAlreadyExists("The given username or email is already used");
            }
            var user = new Models.User
            {
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                UserName = request.Pseudo,
                BirthDate = request.BirthDate
            };
            var addedUser = await userRepository.Add(user, request.Password);
            return new UserDto
            {
                BirthDate = addedUser.BirthDate,
                Pseudo = addedUser.UserName,
                Lastname = addedUser.Lastname,
                Firstname = addedUser.Firstname,
                Id = addedUser.Id.ToString(),
                Token = await tokenService.GenerateToken(addedUser)
            };
        }
    }
}
