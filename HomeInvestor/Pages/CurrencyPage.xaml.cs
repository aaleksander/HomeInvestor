using System;
using LiveCharts;
using LiveCharts.Wpf;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Pages
{
	/// <summary>
	/// Interaction logic for CurrencyPage.xaml
	/// </summary>
	public partial class CurrencyPage : BasePage
	{
		public CurrencyPage(MainViewModel context): base()
		{
			InitializeComponent();
			DataContext = context;

			chart.Series = new LiveCharts.SeriesCollection() { 
                     new LineSeries
                     { 
                         Values = new ChartValues<double> {1, 6, 7, 2, 9, 3, 6, 5} 
                     } 
			};
		}
	}
}

//TODO изучить примеры отсюда https://github.com/Live-Charts/Live-Charts/tree/master/Examples/Wpf