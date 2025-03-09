using Taskly.Natif.Application.Services;

namespace Taskly.Natif.Services
{
    public class NatifNavigationService : INavigationService
    {
        public Task<bool> NavigateTo(string url)
        {
            return Task.FromResult(Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(async () => await Shell.Current.GoToAsync(url, false)) ?? false);
        }
    }
}
