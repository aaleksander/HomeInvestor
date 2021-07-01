using System;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Pages
{
	/// <summary>
	/// Interaction logic for BondPage.xaml
	/// </summary>
	public partial class BondPage : BasePage
	{
		public BondPage(MainViewModel context):base()
		{
			InitializeComponent();
			DataContext = context;
		}
	}
}