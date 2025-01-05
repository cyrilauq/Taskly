using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Taskly.Web.Layout
{
    partial class NavMenu: ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; }

        public bool UserIsAuthenticated = false;
        public string UserName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AuthStateProvider.AuthenticationStateChanged += OnAuthStateChanged;

            await base.OnInitializedAsync();
        }

        private void OnAuthStateChanged(Task<AuthenticationState> task)
        {
            var currentState = task.Result;
            UserIsAuthenticated = currentState.User.Identity != null;
            if (UserIsAuthenticated)
            {
                UserName = currentState.User.Identity.Name;
                StateHasChanged();
            }
        }
    }
}
