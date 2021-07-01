using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace HomeInvestor.Converters
{
	public sealed class LengthPercentageConverter : MarkupExtension, IValueConverter, IMultiValueConverter
	{
		public LengthPercentageConverter() { }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool reverse = false;
			if( parameter != null )
				reverse = parameter.ToString() == "reverse";
			
//			bool reverse = parameter?.ToString() == "reverse";
			if( value != null )
				if (string.IsNullOrWhiteSpace(value.ToString())) 
					return new GridLength(.5, GridUnitType.Star);
			double convertedValue = System.Convert.ToDouble(value);
			if (convertedValue >= 1) return new GridLength(reverse ? 0 : 1, GridUnitType.Star);
			if (convertedValue <= 0) return new GridLength(reverse ? 1 : 0, GridUnitType.Star);
			var gridLength = new GridLength(reverse ? 1 - convertedValue : convertedValue, GridUnitType.Star);
			return gridLength;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool reverse = false;
			if( parameter != null )
				reverse = parameter.ToString() == "reverse";
			if (string.IsNullOrWhiteSpace(values[0].ToString())) 
				return new GridLength(.5, GridUnitType.Star);
			if (values.Length == 1 || values[1] == DependencyProperty.UnsetValue) 
				return Convert(values[0], targetType, parameter, culture);
			if (string.IsNullOrWhiteSpace(values[1].ToString())) 
				return new GridLength(.5, GridUnitType.Star);
			double convertedMin = 0;
			double convertedMax = System.Convert.ToDouble(values[1]);
			if (values.Length > 2 && values[2] != DependencyProperty.UnsetValue)
			{
				convertedMin = convertedMax;
				convertedMax = System.Convert.ToSingle(values[2]);
			}
			Single convertedValue = System.Convert.ToSingle(values[0]);
			if (convertedValue >= convertedMax) 
				return new GridLength(reverse ? 0 : convertedMax - convertedMin, GridUnitType.Star);
			if (convertedValue <= convertedMin) 
				return new GridLength(reverse ? convertedMax - convertedMin : 0, GridUnitType.Star);
			var gridLength = new GridLength(reverse ? 
			                                convertedMax - convertedMin - convertedValue 
			                                : convertedValue - convertedMin, 
			                                GridUnitType.Star);
			return gridLength;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override object ProvideValue(IServiceProvider serviceProvider) { return this; }
	}
}
