using System;
using System.Windows;

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for dlgEtf.xaml
	/// </summary>
	public partial class dlgEtf : Window
	{
		public dlgEtf()
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