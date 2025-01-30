using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Natif.Application.Validator;
using Taskly.Natif.Application.Validator.Rules;

namespace Taskly.Natif.ViewModels
{
    public partial class SaveTodoViewModel : ObservableObject
    {
        private TodoModel? _todo;
        private ITodoService todoService;

        public Action<TodoModel?> ClosePopup { get; set; }

        public TodoModel? Todo 
        { 
            get => _todo; 
            set
            {
                if(value != null)
                {
                    _todo = value;
                    TodoNameValidator.Value = value.Name;
                    TodoContentValidator.Value = value.Content;
                }
            }
        }

        [ObservableProperty]
        private string? _todoName;
        [ObservableProperty]
        private string? _todoContent;
        public ValidatableObject<string> TodoNameValidator { get; init; }
        public ValidatableObject<string> TodoContentValidator { get; init; }

        public SaveTodoViewModel(ITodoService todoService)
        {
            this.todoService = todoService;
            TodoNameValidator = new()
            {
                Rules = new()
                {
                    new MinimumLengthRule(5) { ValidationMessage = "The name should be at least 5 characters long" },
                    new MaximumLengthRule(25) { ValidationMessage = "The name should'nt be longer than 25 characters" }
                }
            };
            TodoContentValidator = new()
            {
                Rules = new()
                {
                    new MaximumLengthRule(100) { ValidationMessage = "The content should'nt be longer than 100 characters" }
                }
            };
        }

        public string BtnText => Todo is null ? "Create" : "Save";
        public string TitleText => Todo is null ? "New todo" : "Todo editing";


        [RelayCommand]
        private async Task OnSaveAsync()
        {
            try
            {
                if (!FormIsValid()) return;
                TodoModel? result;
                if (Todo != null)
                {
                    Todo.Content = (string)TodoContentValidator.Value!;
                    Todo.Name = (string)TodoNameValidator.Value!;
                    result = await todoService.SaveAsync(Todo);
                }
                else
                {
                    result = await todoService.SaveAsync(new() { Content = (string)TodoContentValidator.Value!, Name = (string)TodoNameValidator.Value! });
                }
                TodoContentValidator.Value = "";
                TodoNameValidator.Value = "";
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

        private bool FormIsValid()
        {
            var result = false;
            result |= TodoContentValidator.Validate();
            result |= TodoNameValidator.Validate();
            return !result;
        }
    }
}
