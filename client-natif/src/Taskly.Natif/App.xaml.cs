using Taskly.Natif.Application.ViewModels;

namespace Taskly.Natif
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(MenuViewModel menuViewModel)
        {
            InitializeComponent();

            MainPage = new AppShell(menuViewModel);
        }
    }
}
