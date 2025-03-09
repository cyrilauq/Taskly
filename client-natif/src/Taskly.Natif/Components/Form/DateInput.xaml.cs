using Taskly.Natif.Application.Validator;

namespace Taskly.Natif.Components.Form;

public partial class DateInput : ContentView
{
    public static readonly BindableProperty LabelTextProperty =
             BindableProperty.Create(nameof(LabelText), typeof(string), typeof(DateInput), default);
    public static readonly BindableProperty LabelNameProperty =
             BindableProperty.Create(nameof(LabelName), typeof(string), typeof(DateInput), default);
    public static readonly BindableProperty PlaceholderTextProperty =
             BindableProperty.Create(nameof(PlaceholderText), typeof(DateOnly), typeof(DateInput), default);
    public static readonly BindableProperty ValidatorProperty =
             BindableProperty.Create(nameof(Validator), typeof(IValidatorObject), typeof(DateInput), default);

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
    public DateOnly PlaceholderText
    {
        get => (DateOnly)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }
    public IValidatorObject Validator
    {
        get => (IValidatorObject)GetValue(ValidatorProperty);
        set => SetValue(ValidatorProperty, value);
    }


    public DateInput()
	{
		InitializeComponent();
	}

    private void Input_Changed(object sender, TextChangedEventArgs e)
    {
        ErrorMessage.IsVisible = false;
        ValidateInput(e.NewTextValue);
    }

    private void ValidateInput(string input)
    {
        if (Validator is null) return;
        ErrorMessage.IsVisible = false;
        string? error = null;
        if (Validator.Validate())
        {
            ErrorMessage.Text = Validator.Error;
            ErrorMessage.IsVisible = true;
        }
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {

    }
}