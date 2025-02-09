using System.Windows.Input;

namespace Taskly.Natif.Components.Dashboard;

public partial class DashBoardItemAndroid : ContentView
{
    public static readonly BindableProperty OnUpdateProperty =
             BindableProperty.Create(nameof(OnUpdate), typeof(ICommand), typeof(DashBoardItemAndroid));

    public static readonly BindableProperty OnDeleteProperty =
             BindableProperty.Create(nameof(OnDelete), typeof(ICommand), typeof(DashBoardItemAndroid));

    public static readonly BindableProperty OnMarkTodoProperty =
             BindableProperty.Create(nameof(OnMarkTodo), typeof(ICommand), typeof(DashBoardItemAndroid));

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

    public DashBoardItemAndroid()
	{
		InitializeComponent();
	}
}