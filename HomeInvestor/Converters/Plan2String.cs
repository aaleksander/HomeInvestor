using System;
using System.Windows.Data;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of Plan2String.
	/// </summary>
	public class Plan2String: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var val = (int)value;
			if( val > 0 ) return string.Format("+ {0}", val);
			if( val < 0 ) return string.Format("- {0}", -val);
			return "0";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			throw new Exception("niht!");
        }
	}
}
