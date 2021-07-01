/*
 * Created by SharpDevelop.
 * User: AnufrievAA
 * Date: 15.04.2019
 * Time: 18:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Data;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of OblAmortConverter.
	/// </summary>
	public class OblAmortConverter: IMultiValueConverter
	{
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)   
        {
        	//return null;
        	var t = Tuple.Create<BondViewModel, AmortViewModel>((BondViewModel)values[1], (AmortViewModel)values[0]);
            return t;
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
