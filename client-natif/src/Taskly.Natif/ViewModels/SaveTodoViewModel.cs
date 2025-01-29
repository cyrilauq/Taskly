using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;

namespace Taskly.Natif.ViewModels
{
    public partial class SaveTodoViewModel(ITodoService todoService) : ObservableObject
    {
        private TodoModel? _todo;

        public Action<TodoModel?> ClosePopup { get; set; }

        public TodoModel? Todo 
        { 
            get => _todo; 
            set
            {
                if(value != null)
                {
                    _todo = value;
                    TodoName = value.Name;
                    TodoContent = value.Content;
                }
            }
        }
        [ObservableProperty]
        private string? _todoName;
        [ObservableProperty]
        private string? _todoContent;

        [RelayCommand]
        private async Task OnSaveAsync()
        {
            try
            {
                TodoModel? result;
                if (Todo != null)
                {
                    Todo.Content = TodoContent;
                    Todo.Name = TodoName;
                    result = await todoService.SaveAsync(Todo);
                }
                else
                {
                    result = await todoService.SaveAsync(new() { Content = TodoContent, Name = TodoName });
                    TodoName = "";
                    TodoContent = "";
                    Todo = null;
                }
                TodoName = "";
                TodoContent = "";
                Todo = null;
                ClosePopup.Invoke(result);
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
