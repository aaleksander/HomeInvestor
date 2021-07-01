using System;
using System.Windows;

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for dlgStock.xaml
	/// </summary>
	public partial class dlgStock : Window
	{
		public dlgStock()
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

//TODO сообщать об облигациях, которые погасились, но еще остались в портфелях и справочнике
//TODO в портфеле завести столбец под конкретный тип актив, если облига, то выводить купон и дней до погашения. Если акция - то ближайшие дивиденты и т.п.
//TODO еврооблигации
