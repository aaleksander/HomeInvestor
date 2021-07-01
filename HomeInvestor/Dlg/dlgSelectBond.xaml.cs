using System;
using System.Windows;

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for dlgSelectBond.xaml
	/// </summary>
	public partial class dlgSelectBond : Window
	{
		public dlgSelectBond()
		{
			InitializeComponent();
		}
		void Button_Click(object sender, RoutedEventArgs e)
		{
			if( grid.SelectedItem == null ) //проверяем, что мы что-то выбрали из таблицы
			{
				MessageBox.Show("Выберите облигацию");
				return;
			}	
			DialogResult = true;
		}
	}
}