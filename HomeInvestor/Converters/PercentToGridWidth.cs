/*
 * Created by SharpDevelop.
 * User: anufrievaa
 * Date: 04.04.2019
 * Time: 9:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of PercentToGridWidth.
	/// </summary>
	public class PercentToGridWidth: MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			//Width = new GridLength((double)p.Percent, GridUnitType.Star)
			var val = (decimal)value;
			var par = int.Parse((string)parameter);
//			return new GridLength((double)val/100, GridUnitType.Star);
			switch(par)
			{
				case 1: return new GridLength((double)val/100, GridUnitType.Star);
				case 2: return new GridLength((double)(100 - val)/100, GridUnitType.Star);
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			throw new Exception("niht!");
        }

		public override object ProvideValue(IServiceProvider serviceProvider) { return this; }
	}
}
