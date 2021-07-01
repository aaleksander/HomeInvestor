using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace HomeInvestor.Converters
{
	class DoubleDivideIntConverter : MarkupExtension, IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(values[0] is double) || !(values[1] is int)) return null;
			var size = (double) values[0];
			var count = (int) values[1];
			return size / count;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override object ProvideValue(IServiceProvider serviceProvider) { return this; }
	}
}
