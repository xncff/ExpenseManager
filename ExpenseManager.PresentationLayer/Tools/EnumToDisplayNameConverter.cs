using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace ExpenseManager.PresentationLayer.Tools;

public class EnumToDisplayNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return string.Empty;
        }

        if (value is not Enum enumValue)
        {
            return value.ToString() ?? string.Empty;
        }

        var member = enumValue.GetType() .GetMember(enumValue.ToString()) .FirstOrDefault();

        var displayAttr = member?.GetCustomAttribute<DisplayAttribute>();

        var displayName = displayAttr?.GetName();
        return string.IsNullOrWhiteSpace(displayName) ? enumValue.ToString() : displayName;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}