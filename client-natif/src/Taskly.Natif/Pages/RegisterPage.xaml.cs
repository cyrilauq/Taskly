using Taskly.Natif.ViewModels;

namespace Taskly.Natif.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}