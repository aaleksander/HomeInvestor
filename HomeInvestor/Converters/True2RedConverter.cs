/*
 * Created by SharpDevelop.
 * User: anufrievaa
 * Date: 08.04.2019
 * Time: 11:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of True2RedConverter.
	/// </summary>
	public class True2RedConverter : IValueConverter
	{
	    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	    {
	    	var val = (bool)value;
	    	SolidColorBrush def = null;
	    	if( parameter != null )
	    		def = Brushes.LightGray;	    		
	    	return val? Brushes.Red: def;
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	    {
	        return new SolidColorBrush((Color)value);
	    }
	}
}
