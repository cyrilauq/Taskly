using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;

namespace Taskly.Natif.ViewModels
{
    public partial class DashboardViewModel(ITodoService todoService) : ObservableObject
    {
        [ObservableProperty]
        private IList<TodoModel> _todos;

        [RelayCommand]
        private async Task OnPageLoadedAsync()
        {
            Todos = new List<TodoModel>(await todoService.GetConnectedUserTodos());
        }
    }
}
