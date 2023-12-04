using GAAPICommon.Architecture;
using GACore.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GACore.Controls.View;

public class RadToDegStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double rad = (float)value;
		return rad.RadToDeg();
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		=> throw new NotImplementedException("RadToDegStringConverter ConvertBack()");
}

public class IsVirtualColorConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool isVirtual = (bool)value;
		return isVirtual ? Brushes.Cyan : Brushes.Black;
	}

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException("IsVirtualColorConverter ConvertBack()");
    }
}

public class DynamicLimiterStatusToOverlayTextConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		DynamicLimiterStatus status = (DynamicLimiterStatus)value;
		BrushCollection brushCollection = BrushDictionaries.DynamicLimiterStatusBrushCollectionDictionary.GetBrushCollection(status);

		BrushCollectionProperty property = (BrushCollectionProperty)parameter;
		return brushCollection.GetProperty(property);
	}

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public sealed class NavigationStatusToOverlayTextConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		NavigationStatus status = (NavigationStatus)value;
		BrushCollection brushCollection = BrushDictionaries.NavigationStatusBackgroundBrushCollectionDictionary.GetBrushCollection(status);

		BrushCollectionProperty property = (BrushCollectionProperty)parameter;
		return brushCollection.GetProperty(property);
	}

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PositionControlStatusToOverlayTextConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		PositionControlStatus status = (PositionControlStatus)value;
		BrushCollection brushCollection = BrushDictionaries.PositionControlStatusBackgroundBrushCollectionDictionary.GetBrushCollection(status);

		BrushCollectionProperty property = (BrushCollectionProperty)parameter;
		return brushCollection.GetProperty(property);
	}

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
