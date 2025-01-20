namespace Taskly.Natif.ViewModels.Interfaces
{
    internal interface ILoginViewModel
    {
        string Login { get; set; }
        string Password { get; set; }

        Task<bool> OnLogin();
    }
}
