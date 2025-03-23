namespace Taskly.Natif.Application.Services.Interface
{
    public interface IToastService
    {
        Task ShowMessageAsync(string message);
        Task ShowErrorAsync(string error);
        Task ShowWarningAsync(string warning);
        Task<bool> ShowConfirmationBoxAsync(string caption, string message, string acceptTxt, string cancelTxt);
    }
}
