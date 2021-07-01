using System;
using System.Windows;

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for dlgSelectStock.xaml
	/// </summary>
	public partial class dlgSelectStock : Window
	{
		public dlgSelectStock()
		{
			InitializeComponent();
		}
		void Button_Click(object sender, RoutedEventArgs e)
		{
			if( grid.SelectedItem == null ) //проверяем, что мы что-то выбрали из таблицы
			{
				MessageBox.Show("Выберите акцию");
				return;
			}	
			DialogResult = true;
		}
	}
}