/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 28.10.2019
 * Время: 10:31
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeInvestor.Dlg.AddActiveWizard
{
	/// <summary>
	/// Interaction logic for dlgSelectActiveWizard.xaml
	/// </summary>
	public partial class dlgSelectActive : Window
	{
		public dlgSelectActive()
		{
			InitializeComponent();
		}

		public int Selected{private set;get;}
		
		void Button_Stocks(object sender, RoutedEventArgs e)
		{
			Selected = 1;
			DialogResult = true;
		}

		void Button_Bonds(object sender, RoutedEventArgs e)
		{
			Selected = 2;
			DialogResult = true;
		}
		void Button_Etf(object sender, RoutedEventArgs e)
		{
			Selected = 3;
			DialogResult = true;
		}
	}
}