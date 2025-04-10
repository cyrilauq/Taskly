﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.Application.ViewModels
{
    public partial class DashboardViewModel(ITodoService todoService, IPopupService popupService, ILogger<DashboardViewModel> logger, IToastService toastService) : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TodoModel> _todos;
        [ObservableProperty]
        private bool _makMultipleTodosChecked = false;
        [ObservableProperty]
        private ObservableCollection<MarkOption> _markOptions;

        [RelayCommand]
        private async Task OnPageLoadedAsync()
        {
            try
            {
                // TODO : Make android todo item checkable
                MarkOptions =
                [
                    new("done", "done"),
                    new("not done", "undone")
                ];
                Todos = new ObservableCollection<TodoModel>(await todoService.GetConnectedUserTodos());
            }
            catch (ServiceException se)
            {
                await toastService.ShowErrorAsync(se.Message);
            }
            catch (Exception se)
            {
                await toastService.ShowErrorAsync("Unexpected error");
            }
        }

        [RelayCommand]
        private async Task OnUpdateClicked(string todoId)
        {
            var foundTodo = Todos.First(t => t.Id == todoId);
            var lastIndex = Todos.IndexOf(foundTodo);
            var todo = await popupService.ShowPopupAsync<SaveTodoViewModel, TodoModel>(viewModel => viewModel.Todo = foundTodo);
            if (todo != null)
            {
                Todos[lastIndex] = todo;
            }
        }

        [RelayCommand]
        private async Task OnDeleteAsync(string todoId)
        {

            var confirmationResult = await toastService.ShowConfirmationBoxAsync("Are you sure you to delete the item?", "There is no going back after confirming the action", "Yes", "No");
            if (!confirmationResult) return;
            try
            {
                var deleteResult = await todoService.DeleteAsync(todoId);
                if (deleteResult)
                {
                    Todos.Remove(Todos.First(t => t.Id == todoId));
                }
            }
            catch (ServiceException se)
            {
                await toastService.ShowErrorAsync(se.Message);
            }
            catch (Exception se)
            {
                await toastService.ShowErrorAsync("Unexpected error");
            }
        }

        [RelayCommand]
        private async Task OnNewClickedAsync()
        {
            var todo = await popupService.ShowPopupAsync<SaveTodoViewModel, TodoModel>();
            if (todo != null)
            {
                Todos.Add(todo);
            }
        }

        [RelayCommand]
        private async Task OnMarkTodoAsync(string todoId)
        {
            try
            {
                TodoModel todo = Todos.First(t => t.Id == todoId);
                int todoIndex = Todos.IndexOf(todo);
                bool markResult = await todoService.MarkTodoAsync(Guid.Parse(todo.Id), !todo.IsDone);
                if (markResult)
                {
                    todo.IsDone = !todo.IsDone;
                    Todos[todoIndex] = todo;
                }
            }
            catch (ServiceException se)
            {
                logger.LogInformation(se, se.Message);
                await toastService.ShowErrorAsync("An unexpected error occured");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occured");
                await toastService.ShowErrorAsync("An unexpected error occured");
            }
        }

        [RelayCommand]
        private void SelectMultipleTodoClicked()
        {
            MakMultipleTodosChecked = true;
        }

        [RelayCommand]
        private async Task MarkSelectedTodosAsync(string option)
        {
            try
            {
                bool asDone = option == "done";
                IEnumerable<Guid> todosToMark = Todos.ToList().Where(t => t.IsChecked).Select(t => Guid.Parse(t.Id));
                bool markResult = await todoService.MarkMutipleTodoAsync(todosToMark, asDone);
                if (markResult)
                {
                    foreach (Guid id in todosToMark)
                    {
                        TodoModel originalTodo = Todos.First(t => t.Id == id.ToString());
                        int todoIndex = Todos.IndexOf(originalTodo);
                        originalTodo.IsDone = asDone;
                        Todos[todoIndex] = originalTodo;
                    }
                }
            }
            catch (ServiceException se)
            {
                logger.LogInformation(se, se.Message);
                await toastService.ShowErrorAsync("An unexpected error occured");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occured");
                await toastService.ShowErrorAsync("An unexpected error occured");
            }
        }
    }

    public record MarkOption(string Label, string Key);
}
