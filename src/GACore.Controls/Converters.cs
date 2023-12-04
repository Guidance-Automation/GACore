using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GACore.Controls;

/// <summary>
/// If object is null, visibility is collapsed.
/// bool parameter can be used to invert behavior (default = false)
/// </summary>
public class NullToCollapsedVisibiltyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool invert = false;

        if (parameter is bool v)
            invert = v;

        if (invert)
            return value == null
                ? Visibility.Visible
                : Visibility.Collapsed;
        else
        {
            return value == null
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// If object is null, visibility is hidden.
/// bool parameter can be used to invert behavior (default = false)
/// </summary>
public class NullToHiddenVisibiltyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool invert = false;

        if (parameter is bool v)
            invert = v;

        if (invert)
            return value == null
                ? Visibility.Visible
                : Visibility.Hidden;
        else
            return value == null
                ? Visibility.Hidden
                : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class IPAddressToByteConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        IPAddress ipAddress = (IPAddress)value;

        byte[] bytes = ipAddress.GetAddressBytes();
        int index = int.Parse((string)parameter);

        return bytes[index];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class IsTrueColorConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isTrue = (bool)value;

        return isTrue
            ? (SolidColorBrush?)(new BrushConverter().ConvertFrom("#C4D92E"))
            : Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NullableEnumStringConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null
            ? "null"
            : value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NullableBoolButtonStateStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((bool?)value)
        {
            case true: return "Status: Pressed";

            case false: return "Status: De-pressed";

            default:
            case null: return "Status: Unknown";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NullableBoolStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((bool?)value)
        {
            case true: return "True";

            case false: return "False";

            default:
            case null: return "null";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}