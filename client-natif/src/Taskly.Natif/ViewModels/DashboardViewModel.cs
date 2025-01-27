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
    public partial class DashboardViewModel(ITodoService todoService) : ObservableObject
    {
        [ObservableProperty]
        private string? _todoName;
        [ObservableProperty]
        private string? _todoContent;
        [ObservableProperty]
        private bool _createFormVisible;
        [ObservableProperty]
        private ObservableCollection<TodoModel> _todos;
        [ObservableProperty]
        private TodoModel? _todo;

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
        private async Task OnCreateAsync()
        {
            try
            {

                if (Todo != null)
                {
                    Todo.Content = TodoContent;
                    Todo.Name = TodoName;
                    var saveResult = await todoService.SaveAsync(Todo);
                    var oldTodoPos = Todos.IndexOf(Todo);
                    Todos[oldTodoPos] = saveResult;
                }
                else
                {
                    var saveResult = await todoService.SaveAsync(new() { Content = TodoContent, Name = TodoName });
                    TodoName = "";
                    TodoContent = "";
                    Todo = null;
                    CreateFormVisible = false;
                    Todos.Add(saveResult);
                }
                TodoName = "";
                TodoContent = "";
                Todo = null;
                CreateFormVisible = false;
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
        private void OnCreateClicked()
        {
            CreateFormVisible = true;
            Todo = null;
            TodoName = "";
            TodoContent = "";
        }

        [RelayCommand]
        private void OnUpdateClicked(string todoId)
        {
            CreateFormVisible = true;
            var todo = Todos.First(t => t.Id == todoId);
            Todo = todo;
            TodoName = Todo.Name;
            TodoContent = Todo.Content;
        }

        [RelayCommand]
        private async Task OnDeleteAsync(string todoId)
        {
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
    }
}
