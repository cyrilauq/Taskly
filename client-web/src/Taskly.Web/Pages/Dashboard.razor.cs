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

        public TodoModel Todo { get; set; } = new TodoModel();
        public int TodoCount { get; set; } = 0;
        public required EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(Todo);

            base.OnInitialized();
        }

        public async Task OnSubmit()
        {
            try
            {
                await TodoService.CreateAsync(Todo);
                TodoCount++;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}
