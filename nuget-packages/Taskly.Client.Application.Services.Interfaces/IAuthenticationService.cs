using Taskly.Client.Application.Model;

namespace Taskly.Client.Application.Services.Interfaces
{
    /// <summary>
    /// Authentication service abstraction
    /// </summary>
    /// <typeparam name="L">Model for the login functionnality</typeparam>
    /// <typeparam name="R">Model for the register functionnality</typeparam>
    public interface IAuthenticationService<L, R>
    {
        Task<bool> LoginWithCredentials(L loginModel);
        Task<bool> RegisterUser(R registerModel);
    }

    /// <summary>
    /// Default attribution of login and register model
    /// </summary>
    public interface IAuthenticationService : IAuthenticationService<LoginModel, RegisterModel> { }
}
