/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 13.02.2020
 * Время: 14:39
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
	/// Interaction logic for VDOPage.xaml
	/// </summary>
	public partial class VDOPage : BasePage
	{
		public VDOPage(MainViewModel context):base()
		{
			InitializeComponent();
			DataContext = context;
		}
	}
}