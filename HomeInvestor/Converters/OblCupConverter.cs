/*
 * Created by SharpDevelop.
 * User: AnufrievAA
 * Date: 11.04.2019
 * Time: 9:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Converters
{	
	/// <summary>
	/// Description of OblCupConverter.
	/// </summary>
	public class OblCupConverter: IMultiValueConverter
	{
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)   
        {   
        	var t = Tuple.Create<BondViewModel, CuponViewModel>((BondViewModel)values[1], (CuponViewModel)values[0]);
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
