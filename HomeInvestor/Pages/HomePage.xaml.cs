/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 07.11.2019
 * Время: 17:16
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Pages
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : BasePage
	{
		public HomePage(MainViewModel context): base()
		{
			InitializeComponent();
			DataContext = context;
		}

		void PieChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
		{
             var chart = (LiveCharts.Wpf.PieChart) chartPoint.ChartView; 
             chart.Series.Clear();
             chart.Series = new SeriesCollection 
             {
                 new PieSeries 
                 { 
                     Title = "Chrome", 
                     Values = new ChartValues<ObservableValue> { new ObservableValue(8) }, 
                     DataLabels = true,
                 },
                 new PieSeries 
                 { 
                     Title = "Mozilla", 
                     Values = new ChartValues<ObservableValue> { new ObservableValue(6) }, 
                     DataLabels = true 
                 },
                 new PieSeries 
                 { 
                     Title = "Opera", 
                     Values = new ChartValues<ObservableValue> { new ObservableValue(10) }, 
                     DataLabels = true 
                 },
                 new PieSeries 
                 { 
                     Title = "Explorer", 
                     Values = new ChartValues<ObservableValue> { new ObservableValue(4) }, 
                     DataLabels = true 
                 } 
             };

             //clear selected slice. 
//             foreach (PieSeries series in chart.Series) 
//                 series.PushOut = 0; 
 
//              var selectedSeries = (PieSeries) chartPoint.SeriesView; 
//             selectedSeries.PushOut = 8; 
		}
	}
}