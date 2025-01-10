using Taskly.Web.Infrastructure.DTO;

namespace Taskly.Web.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDTO> GetByCredentials(LoginArgs args);

        public Task<UserDTO> Add(RegisterArgs args);
    }

    public record LoginArgs(string Login, string Password);
    
    public record RegisterArgs(string Firstname, string Lastname, string Pseudo, string Email, string Password, DateOnly BirthDate);
}
