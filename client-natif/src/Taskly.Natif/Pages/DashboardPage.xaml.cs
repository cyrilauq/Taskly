using CommunityToolkit.Maui.Views;
using Taskly.Client.Application.Model;
using Taskly.Natif.ViewModels;

namespace Taskly.Natif.Pages;

public partial class DashboardPage : ContentPage
{
	private SaveTodoViewModel _saveTodoViewModel;


    public DashboardPage(DashboardViewModel vm, SaveTodoViewModel saveTodoViewModel)
	{
		InitializeComponent();

		BindingContext = vm;
		_saveTodoViewModel = saveTodoViewModel;
    }

	public async void OnCreate_Clicked(object sender, EventArgs e)
	{
		var result = (TodoModel?)(await this.ShowPopupAsync(new SaveTodoPage(_saveTodoViewModel), CancellationToken.None));
		if (result != null)
		{
            ((DashboardViewModel)BindingContext).OnTodoAdded(result);
        }
	}

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}