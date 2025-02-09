namespace Taskly.Natif.Application.Services.Interface
{
    public interface IToastService
    {
        Task ShowMessageAsync(string message);
        Task ShowErrorAsync(string message);
    }
}
