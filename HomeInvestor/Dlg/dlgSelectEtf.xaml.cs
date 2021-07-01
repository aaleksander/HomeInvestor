using System;
using System.Windows;

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for dlgSelectEtf.xaml
	/// </summary>
	public partial class dlgSelectEtf : Window
	{
		public dlgSelectEtf()
		{
			InitializeComponent();
		}
		void Button_Click(object sender, RoutedEventArgs e)
		{
			if( grid.SelectedItem == null ) //проверяем, что мы что-то выбрали из таблицы
			{
				MessageBox.Show("Выберите ETF");
				return;
			}			
			DialogResult = true;
		}
	}
}