/*
 * Created by SharpDevelop.
 * User: AnufrievAA
 * Date: 08.05.2019
 * Time: 13:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of Plan2Color.
	/// </summary>
	public class Plan2Color: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var val = (int)value;
			if( val > 0 ) return Brushes.Green;
			if( val < 0 ) return Brushes.Red;
			return Brushes.Gray;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			throw new Exception("niht!");
        }
	}
}
