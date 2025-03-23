using CommunityToolkit.Maui.Views;
using Taskly.Client.Application.Model;
using Taskly.Natif.Application.ViewModels;

namespace Taskly.Natif.Pages;

public partial class SaveTodoPage : Popup
{
	public SaveTodoPage(SaveTodoViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
		vm.ClosePopup = async (TodoModel? todo) => await CloseAsync(todo, CancellationToken.None);
	}
}