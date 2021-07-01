using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Linq;
using HomeInvestor.Actors;
using HomeInvestor.ViewModels;

//FIXME !!! если поменять объем позиции, то на общем портфеле это никак не отразится

namespace HomeInvestor
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
            var ci = new CultureInfo("ru-RU", true);
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;			

			InitializeComponent();

			_dataContext = new MainViewModel();
			DataContext = _dataContext;

			_actor.OnAddPortfolio += p =>
			{
//				App.Current.Dispatcher.Invoke(() => 
//				                              {
//													var tnew = new TabPortfolio(p);
//													tabs.Items.Add(tnew);
//													tabs.SelectedValue = tnew;
//				                              });
			};

			_actor.OnRemovePortfolio += p =>
			{
//				Debug.WriteLine("Удаляем вкладку портфеля");
//				App.Current.Dispatcher.Invoke(() => 
//				                              {
//				                              	foreach(TabPortfolio t in tabs.Items)
//				                              	{
//				                              		if( t.DataContext == p )
//				                              		{
//				                              			tabs.Items.Remove(t);
//				                              			break;
//				                              		}
//				                              	}
//				                              });
			};
		}
		MainViewModel _dataContext = null;
		MainWindowActor _actor = new MainWindowActor();

		void Window_Closed(object sender, EventArgs e)
		{
			ActorSystem.Instance.Dispose();
		}

		void Window_Loaded(object sender, RoutedEventArgs e)
		{
//			foreach(var p in _dataContext.Portfolios)
//			{
//				var ti = new TabPortfolio(p);
//				tabs.Items.Insert(tabs.Items.Count, ti); 
//			}
//
//			tabs.SelectedIndex = 0;
		}
	}
}