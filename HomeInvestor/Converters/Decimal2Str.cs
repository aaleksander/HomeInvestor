/*
 * Created by SharpDevelop.
 * User: anufrievaa
 * Date: 04.04.2019
 * Time: 14:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;




namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of Decimal2Str.
	/// </summary>
	public class Decimal2Str: MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if(value is decimal)
			{
				var val = (decimal)value;
				var f = new NumberFormatInfo(){NumberGroupSeparator = " "};
				return val.ToString("n", f);
			}

			if( value is int )
			{
				var val = (int)value;
				var f = new NumberFormatInfo(){NumberGroupSeparator = " "};
				return val.ToString("n0", f);
			}

			if( value is double )
			{
				var val = (double)value;
				var f = new NumberFormatInfo(){NumberGroupSeparator = " "};
				return val.ToString("n", f);
			}

			if( value == null )
			{
				return "";
			}

			return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			if( targetType == typeof(Int32) )
			{
				var str = (string)value;
				str = str.Replace(" ", "");
				return int.Parse(str);
			}

			throw new Exception("niht!");
        }

		public override object ProvideValue(IServiceProvider serviceProvider) { return this; }
	}
}
