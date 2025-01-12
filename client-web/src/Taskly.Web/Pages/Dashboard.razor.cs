using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services.Interfaces;

namespace Taskly.Web.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        private ITodoService TodoService { get; set; }
        [Inject]
        private ILogger<Dashboard> Logger { get; set; }

        private Modal newTodoModal = default!;
        public TodoModel Todo { get; set; } = new TodoModel();
        public IEnumerable<TodoModel> Todos { get; set; } = [];
        public int TodoCount { get; set; } = 0;
        public required EditContext EditContext { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(Todo);

            Todos = await TodoService.GetConnectedUserTodos();

            await base.OnInitializedAsync();
        }

        public async Task OnSubmit()
        {
            try
            {
                await TodoService.CreateAsync(Todo);
                TodoCount++;
                Todo = new TodoModel();
                await newTodoModal.HideAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }

        private async Task OnShowModalClick()
        {
            await newTodoModal.ShowAsync();
        }
    }
}
