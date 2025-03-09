namespace Taskly.Natif.Application.Services
{
    public interface INavigationService
    {
        Task<bool> NavigateTo(string url);
    }
}
