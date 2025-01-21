using Taskly.Natif.Pages;

namespace Taskly.Natif
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("auth/login", typeof(LoginPage));
            Routing.RegisterRoute("dashboard", typeof(DashboardPage));
        }
    }
}
