using Taskly.Client.Application.Model;

namespace Taskly.Client.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> LoginWithCredentials(string login, string password);
        Task<bool> RegisterUser(RegisterModel registerModel);
    }
}
