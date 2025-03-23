namespace Taskly.Natif.Application.ViewModels.Interfaces
{
    internal interface ILoginViewModel
    {
        string Login { get; set; }
        string Password { get; set; }

        Task<bool> OnLogin();
    }
}
