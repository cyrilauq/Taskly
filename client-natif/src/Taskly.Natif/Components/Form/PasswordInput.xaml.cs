using Taskly.Natif.Application.Validator;

namespace Taskly.Natif.Components.Form;

public partial class PasswordInput : ContentView
{
    public static readonly BindableProperty LabelTextProperty =
             BindableProperty.Create(nameof(LabelText), typeof(string), typeof(PasswordInput), default);
    public static readonly BindableProperty LabelNameProperty =
             BindableProperty.Create(nameof(LabelName), typeof(string), typeof(PasswordInput), default);
    public static readonly BindableProperty PlaceholderTextProperty =
             BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(PasswordInput), default);
    public static readonly BindableProperty ValidatorProperty =
             BindableProperty.Create(nameof(Validator), typeof(IValidatorObject), typeof(PasswordInput), default);

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }
    public string LabelName
    {
        get => (string)GetValue(LabelNameProperty);
        set => SetValue(LabelNameProperty, value);
    }
    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }
    public IValidatorObject Validator
    {
        get => (IValidatorObject)GetValue(ValidatorProperty);
        set => SetValue(ValidatorProperty, value);
    }


    public PasswordInput()
	{
		InitializeComponent();
	}
}