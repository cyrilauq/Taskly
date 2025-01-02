namespace Taskly.Web.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> LoginWithCredentials(string login, string password);
    }
}
