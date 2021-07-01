/*
 * Created by SharpDevelop.
 * User: AnufrievAA
 * Date: 25.03.2019
 * Time: 15:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Data;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of Str2DT.
	/// </summary>
	public class Str2DT: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var val = (DateTime)value;			
			return val.ToString("dd.MM.yyyy");
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			var val = (string)value;
			try
			{
				return DateTime.ParseExact(val, "dd.MM.yyyy", null);
			}catch(Exception)
			{
				MessageBox.Show("Дата имеет неверный формат");
				return null;
			}
        }
	}
}
