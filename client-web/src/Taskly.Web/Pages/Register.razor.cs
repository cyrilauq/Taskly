using Microsoft.AspNetCore.Components;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Exceptions;

namespace Taskly.Web.Pages
{
    partial class Register: ComponentBase
    {
        [Inject]
        IAuthenticationService AuthenticationService { get; set; }

        RegisterModel FormModel = new();
        public string? ErrorMessage;

        public async Task OnSubmit()
        {
            try
            {
                ErrorMessage = null;
                await AuthenticationService.RegisterUser(FormModel);
                NavigationManager.NavigateTo("/dashboard");
            }
            catch(ValidationException ve)
            {
                ErrorMessage = ve.Message;
            }
            catch(ResourceAlreadyExists ve)
            {
                ErrorMessage = "A user with the given email or pseudo already exists";
            }
        }
    }
}
