/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 12.11.2019
 * Время: 16:08
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

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for dlgBond.xaml
	/// </summary>
	public partial class dlgBond : Window
	{
		public dlgBond()
		{
			InitializeComponent();
		}
		void Button_Click(object sender, RoutedEventArgs e)
		{
			//TODO добавить проверки полей
			DialogResult = true;
		}
	}
}