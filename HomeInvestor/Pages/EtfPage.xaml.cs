using System;
using HomeInvestor.ViewModels;


namespace HomeInvestor.Pages
{
	/// <summary>
	/// Interaction logic for EtfPage.xaml
	/// </summary>
	public partial class EtfPage : BasePage
	{
		public EtfPage(MainViewModel context):base()
		{
			InitializeComponent();
			DataContext = context;
		}
	}
}