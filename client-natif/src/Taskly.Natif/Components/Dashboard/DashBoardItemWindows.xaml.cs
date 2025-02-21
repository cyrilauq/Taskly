using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Taskly.Natif.Components.Dashboard;

public partial class DashBoardItemWindows : ContentView
{
    public static readonly BindableProperty OnUpdateProperty =
             BindableProperty.Create(nameof(OnUpdate), typeof(ICommand), typeof(DashBoardItemWindows));

    public static readonly BindableProperty OnDeleteProperty =
             BindableProperty.Create(nameof(OnDelete), typeof(ICommand), typeof(DashBoardItemWindows));

    public static readonly BindableProperty OnMarkTodoProperty =
             BindableProperty.Create(nameof(OnMarkTodo), typeof(ICommand), typeof(DashBoardItemWindows));

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

    public ICommand OnMarkTodo
    {
        get => (ICommand)GetValue(OnMarkTodoProperty);
        set => SetValue(OnMarkTodoProperty, value);
    }

    public DashBoardItemWindows()
    {
        InitializeComponent();
    }
}