using System;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Pages
{
	/// <summary>
	/// Interaction logic for CommonPortfolioPage.xaml
	/// </summary>
	public partial class CommonPortfolioPage : BasePage
	{
		public CommonPortfolioPage(MainViewModel context): base()
		{
			InitializeComponent();
			DataContext = context;
		}
	}
}