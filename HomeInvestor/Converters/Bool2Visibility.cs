using System;
using System.Windows;
using System.Windows.Data;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of Bool2Visibility.
	/// </summary>
	public class Bool2Visibility: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var val = (bool)value;
			return val? Visibility.Visible: Visibility.Hidden;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			throw new Exception("niht!");
        }
	}
}
