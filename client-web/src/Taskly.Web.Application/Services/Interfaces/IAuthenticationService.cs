using Taskly.Web.Application.Model;

namespace Taskly.Web.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> LoginWithCredentials(string login, string password);
        Task<bool> RegisterUser(RegisterModel registerModel);
    }
}
