/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 08.11.2019
 * Время: 13:44
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
using HomeInvestor.ViewModels;

namespace HomeInvestor.Pages
{
	/// <summary>
	/// Interaction logic for StockPage.xaml
	/// </summary>
	public partial class StockPage : BasePage
	{
		public StockPage(MainViewModel context): base()
		{
			InitializeComponent();
			DataContext = context;
		}
	}
}