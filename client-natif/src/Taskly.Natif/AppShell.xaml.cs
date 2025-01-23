using Taskly.Natif.Pages;
using Taskly.Natif.ViewModels;

namespace Taskly.Natif
{
    public partial class AppShell : Shell
    {
        public AppShell(MenuViewModel menuViewModel)
        {
            InitializeComponent();

            Routing.RegisterRoute("auth/login", typeof(LoginPage));

            BindingContext = menuViewModel;
        }
    }
}
