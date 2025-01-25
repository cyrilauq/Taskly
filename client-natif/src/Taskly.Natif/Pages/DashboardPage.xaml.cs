using Taskly.Natif.ViewModels;

namespace Taskly.Natif.Pages;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
    }

    private void CreateBtn_Clicked(object sender, EventArgs e)
    {
        CreateForm.IsVisible = !CreateForm.IsVisible;
    }
}