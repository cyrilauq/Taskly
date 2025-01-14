using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Application.State.Interfaces;
using Taskly.Web.Exceptions;
using Taskly.Web.Application.Model;
using Taskly.Web.Shared;

namespace Taskly.Web.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        public required IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public required IAuthState AuthState { get; set; }
        [Inject]
        public required NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; }

        [CascadingParameter]
        public ProcessError ProcessError { get; set; }

        public LoginModel LoginModel { get; set; } = new LoginModel();
        public required EditContext EditContext { get; set; }
        public bool HasError { get => Error != null; }
        public string? Error = null;

        protected override void OnInitialized()
        {
            EditContext = new EditContext(LoginModel);

            base.OnInitialized();
        }

        public async Task OnSubmit()
        {
            ResetError();
            try 
            {
                if (await AuthenticationService.LoginWithCredentials(LoginModel.Login, LoginModel.Password))
                {
                    // If not done, then state isn't aware of its changes
                    await AuthStateProvider.GetAuthenticationStateAsync();
                    NavigationManager.NavigateTo("/dashboard");
                }
            }
            catch(NotFoundException)
            {
                SetError("No user found for the given credentials");
            }
        }

        private void ResetError()
        {
            Error = null;
        }

        private void SetError(string error)
        {
            Error = error;
        }
    }
}
