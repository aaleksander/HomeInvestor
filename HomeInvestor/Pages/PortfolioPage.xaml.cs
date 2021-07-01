using System;
using System.Diagnostics;
using System.Windows.Controls;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Pages
{
	/// <summary>
	/// Interaction logic for PortfolioPage.xaml
	/// </summary>
	public partial class PortfolioPage : BasePage
	{
		public PortfolioPage(PortfolioViewModel dc):base()
		{
			InitializeComponent();
			DataContext = dc;
		}
	}
}