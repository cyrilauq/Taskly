using Microsoft.Maui.Layouts;
using Taskly.Natif.Helpers;
using Taskly.Natif.ViewModels;

namespace Taskly.Natif.Pages;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel vm, SaveTodoViewModel saveTodoViewModel)
	{
		InitializeComponent();

		BindingContext = vm;
    }

    private void MarkBtn_Clicked(object sender, EventArgs e)
    {
#if ANDROID
        if (MenuFrame.IsVisible)
        {
            MenuFrame.IsVisible = false;
            return;
        }

        var button = (VisualElement)sender;
        var buttonPosition = MarkBtn.GetBoundsRelativeToParent(this); 

        // Set MenuFrame position relative to the button
        double menuX = buttonPosition.X;
        double menuY = buttonPosition.Y + button.Height + 5; // Offset for spacing

        // Update AbsoluteLayout positioning
        AbsoluteLayout.SetLayoutBounds(MenuFrame, new Rect(menuX, menuY, MarkBtn.Width + MarkImgBtn.Width, -1));

        MenuFrame.IsVisible = true;
#endif
    }

    private void CloseBtnMenu(object sender, TappedEventArgs e)
    {
        MenuFrame.IsVisible = false;
    }
}