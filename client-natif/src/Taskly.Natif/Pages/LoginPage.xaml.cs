using Taskly.Natif.Application.ViewModels;

namespace Taskly.Natif.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
	}
}