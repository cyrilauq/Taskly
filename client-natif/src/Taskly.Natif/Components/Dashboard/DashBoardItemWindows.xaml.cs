using System.Windows.Input;
using Taskly.Client.Application.Model;

namespace Taskly.Natif.Components.Dashboard;

public partial class DashBoardItemWindows : ContentView
{
    public static readonly BindableProperty OnUpdateProperty =
             BindableProperty.Create(nameof(OnUpdate), typeof(ICommand), typeof(DashBoardItemWindows));

    public static readonly BindableProperty OnDeleteProperty =
             BindableProperty.Create(nameof(OnDelete), typeof(ICommand), typeof(DashBoardItemWindows));

    public ICommand OnUpdate
    {
        get => (ICommand)GetValue(OnUpdateProperty);
        set => SetValue(OnUpdateProperty, value);
    }

    public ICommand OnDelete
    {
        get => (ICommand)GetValue(OnDeleteProperty);
        set => SetValue(OnDeleteProperty, value);
    }

    public DashBoardItemWindows()
	{
		InitializeComponent();
	}
}