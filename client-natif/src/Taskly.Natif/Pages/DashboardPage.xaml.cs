using Taskly.Natif.ViewModels;

namespace Taskly.Natif.Pages;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
    }
}