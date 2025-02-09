using System.Globalization;

namespace Taskly.Natif.Converters
{
    internal class BoolToTextConverter : IValueConverter
    {
        public required string TrueText { get; set; }
        public required string FalseText { get; set; }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isDone = value is null ? false : (bool)value;
            return isDone ? TrueText : FalseText;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
