using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.Services
{
    class NatifToastService : IToastService
    {
        public Task<bool> ShowConfirmationBoxAsync(string caption, string message, string acceptTxt, string cancelTxt)
        {
            return Shell.Current.CurrentPage.DisplayAlert(caption, message, acceptTxt, cancelTxt);
        }

        public async Task ShowErrorAsync(string error)
        {
            var toast = Toast.Make("Unexpected error", ToastDuration.Long, 14);
            await toast.Show();
        }

        public Task ShowMessageAsync(string message)
        {
            throw new NotImplementedException();
        }

        public Task ShowWarningAsync(string warning)
        {
            throw new NotImplementedException();
        }
    }
}
