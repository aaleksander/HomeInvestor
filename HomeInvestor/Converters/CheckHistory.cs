/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 13.03.2020
 * Время: 10:32
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Markup;

namespace HomeInvestor.Converters
{
	/// <summary>
	/// Description of CheckHistory.
	/// </summary>
	public class CheckHistory: MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var val = (bool)value;
			return val? Brushes.Green: Brushes.Red;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			throw new Exception("niht!");
        }

		public override object ProvideValue(IServiceProvider serviceProvider) { return this; }
	}
}
