/*
 * Created by SharpDevelop.
 * User: AnufrievAA
 * Date: 17.04.2019
 * Time: 13:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of NCDConverter.
	/// </summary>
	public class NCDConverter: IMultiValueConverter
	{
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)   
        {   
        	if( values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue )
        		return 0;
        	var ncd = (decimal)values[0];
			var maxNcd = (decimal)values[1];
			var par = (string)parameter;

			if( maxNcd == 0 )
				return 0;

			switch(par)
			{
				case "1":
					return new GridLength((double)ncd/(double)maxNcd, GridUnitType.Star);
				case "2": 
					return new GridLength((double)(maxNcd - ncd)/(double)maxNcd, GridUnitType.Star);
			}
			throw new Exception("Неизвестный параметр");
        }   

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)   
        {   
//            string[] values=null;   
//            if (value != null)   
//                return values = value.ToString().Split(' ');   
            return null; //values 
        }
	}
}
