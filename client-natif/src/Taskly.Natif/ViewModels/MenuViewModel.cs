using CommunityToolkit.Mvvm.ComponentModel;
using Taskly.Client.Application.State.Interfaces;

namespace Taskly.Natif.ViewModels
{
    public partial class MenuViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? connectedUser;

        public MenuViewModel(IAuthState authState)
        {
            authState.OnStateChange += () =>
            {
                ConnectedUser = authState.UserName;
            };
        }
    }
}
