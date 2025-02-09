using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.Services
{
    public class ToastService : IToastService
    {
        public async Task ShowErrorAsync(string message)
        {
            var toast = Toast.Make("Unexpected error", ToastDuration.Long, 14);
            await toast.Show();
        }

        public async Task ShowMessageAsync(string message)
        {
            var toast = Toast.Make("Unexpected error", ToastDuration.Short, 14);
            await toast.Show();
        }
    }
}
