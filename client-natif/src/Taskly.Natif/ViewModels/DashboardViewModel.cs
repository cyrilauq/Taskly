using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Collections.ObjectModel;

namespace Taskly.Natif.ViewModels
{
    public partial class DashboardViewModel(ITodoService todoService, IPopupService popupService) : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TodoModel> _todos;

        [RelayCommand]
        private async Task OnPageLoadedAsync()
        {
            try
            {
                Todos = new ObservableCollection<TodoModel>(await todoService.GetConnectedUserTodos());
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

        [RelayCommand]
        private async Task OnUpdateClicked(string todoId)
        {
            var foundTodo = Todos.First(t => t.Id == todoId);
            var lastIndex = Todos.IndexOf(foundTodo);
            var todo = (TodoModel?)await popupService.ShowPopupAsync<SaveTodoViewModel>(onPresenting: viewModel => viewModel.Todo = foundTodo);
            if(todo != null)
            {
                Todos[lastIndex] = todo;
            }
        }

        [RelayCommand]
        private async Task OnDeleteAsync(string todoId)
        {

            var confirmationResult = await Shell.Current.DisplayAlert("Are you sure you to delete the item?", "There is no going back after confirming the action", "Yes", "No");
            if (!confirmationResult) return;
            try
            {
                var deleteResult = await todoService.DeleteAsync(todoId);
                if(deleteResult)
                {
                    Todos.Remove(Todos.First(t => t.Id == todoId));
                }
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

        [RelayCommand]
        private async Task OnNewClickedAsync()
        {
            var todo = (TodoModel?)await popupService.ShowPopupAsync<SaveTodoViewModel>();
            if(todo != null)
            {
                Todos.Add(todo);
            }
        }
    }
}
