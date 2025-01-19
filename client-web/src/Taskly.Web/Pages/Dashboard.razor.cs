using BlazorBootstrap;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Taskly.Web.Application.Exceptions;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Exceptions;

namespace Taskly.Web.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        private ITodoService TodoService { get; set; }
        [Inject]
        private ILogger<Dashboard> Logger { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }

        private Modal newTodoModal = default!;
        public TodoModel Todo { get; set; } = new TodoModel();
        public IList<TodoModel> Todos { get; set; } = [];
        public int TodoCount { get; set; } = 0;
        public required EditContext EditContext { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(Todo);
            
            await LoadTodos();

            await base.OnInitializedAsync();
        }

        private async Task LoadTodos()
        {
            try
            {
                Todos = (await TodoService.GetConnectedUserTodos()).ToList();
            }
            catch (ServiceException ex)
            {
                ToastService.ShowError(ex.Message);
            }
            catch (Exception)
            {
                ToastService.ShowError("Un unexpected error occured");
            }
        }

        public async Task OnSubmit()
        {
            try
            {
                TodoModel SavedTodo = await TodoService.SaveAsync(Todo);
                if (Todo.Id == null)
                {
                    Todos.Add(SavedTodo);
                }
                else
                {
                    int indexOfPrevious = Todos.IndexOf(Todo);
                    Todos[indexOfPrevious] = SavedTodo;
                }
                Todo = new TodoModel();
                await newTodoModal.HideAsync();
            }
            catch (ServiceException ex)
            {
                ToastService.ShowError(ex.Message);
            }
            catch (Exception)
            {
                ToastService.ShowError("Un unexpected error occured");
            }
        }

        private async Task OnShowModalClick()
        {
            await newTodoModal.ShowAsync();
        }

        private async Task OnDeleteClicked(string todoId)
        {
            try
            {
                await TodoService.DeleteAsync(todoId);
                Todos.Remove(Todos.First(t => t.Id == todoId));
            }
            catch (ServiceException ex)
            {
                ToastService.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Un unexpected error occured");
            }
        }

        private async Task OnUpdateTodoClicked(string todoId)
        {
            Todo = Todos.First(t => t.Id == todoId);
            EditContext = new EditContext(Todo);
            await newTodoModal.ShowAsync();
        }
    }
}
