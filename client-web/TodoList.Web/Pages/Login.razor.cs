using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using TodoList.Web.Application.Services.Interfaces;
using TodoList.Web.Model;

namespace TodoList.Web.Pages
{
    public partial class Login
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        public LoginModel LoginModel { get; set; } = new LoginModel();
        public EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(LoginModel);

            base.OnInitialized();
        }

        public async Task OnSubmit()
        {
            if (await AuthenticationService.LoginWithCredentials(LoginModel.Login, LoginModel.Password))
            {
                Debug.WriteLine("Successfully logged in");
            }
        }
    }
}
