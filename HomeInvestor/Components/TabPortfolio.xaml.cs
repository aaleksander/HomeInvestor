using System;
using System.Windows.Controls;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Components
{
	/// <summary>
	/// Interaction logic for TabPortfolio.xaml
	/// </summary>
	public partial class TabPortfolio : TabItem
	{
		public TabPortfolio(PortfolioViewModel context)
		{
			InitializeComponent();
			_context = context;
			DataContext = _context;
		}

		PortfolioViewModel _context;
	}
}