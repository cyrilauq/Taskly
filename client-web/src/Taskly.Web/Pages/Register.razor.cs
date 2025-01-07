using Microsoft.AspNetCore.Components;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services.Interfaces;

namespace Taskly.Web.Pages
{
    partial class Register: ComponentBase
    {
        [Inject]
        IAuthenticationService AuthenticationService { get; set; }

        RegisterModel FormModel = new();

        public async Task OnSubmit()
        {
            await AuthenticationService.RegisterUser(FormModel);
        }
    }
}
