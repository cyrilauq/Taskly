using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Taskly.Natif.ViewModels
{
    public partial class DashboardViewModel(ITodoService todoService) : ObservableObject
    {
        [ObservableProperty]
        private IList<TodoModel> _todos;

        [RelayCommand]
        private async Task OnPageLoadedAsync()
        {
            try
            {
                // TODO : Display delete button when todo is swiped from left to right
                // TODO : Display edit button when todo is swiped from rigth to left
                Todos = new List<TodoModel>(await todoService.GetConnectedUserTodos());
            }
            catch (ServiceException se)
            {
                var toast = Toast.Make(se.Message, ToastDuration.Long, 14);
                await toast.Show();
            }
            catch (Exception se)
            {
                var toast = Toast.Make("Unexpected error", ToastDuration.Long, 14);
                await toast.Show();
            }
        }
    }
}
