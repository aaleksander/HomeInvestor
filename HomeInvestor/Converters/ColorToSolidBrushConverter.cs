/*
 * Created by SharpDevelop.
 * User: AnufrievAA
 * Date: 29.03.2019
 * Time: 10:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of ColorToSolidBrushConverter.
	/// </summary>
	public class ColorToSolidBrushConverter : IValueConverter
	{
	    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	    {
	    	if( value is SolidColorBrush )
	    		return value;
	        return new SolidColorBrush((Color)value);
	    }
	
	    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	    {
	        return new SolidColorBrush((Color)value);
	    }
	}
}
