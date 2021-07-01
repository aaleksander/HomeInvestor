using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using HomeInvestor.Actors;
using HomeInvestor.ViewModels;


namespace HomeInvestor.Components
{
	/// <summary>
	/// Interaction logic for RectDiagramm.xaml
	/// </summary>
	public partial class RectDiagramm : UserControl
	{
		public RectDiagramm()
		{
			InitializeComponent();
		}		

		public IEnumerable ItemsSource
		{
		    get { return (IEnumerable)GetValue(ItemsSourceProperty); }
		    set { SetValue(ItemsSourceProperty, value); }
		}

		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(RectDiagramm), new PropertyMetadata(new PropertyChangedCallback(OnItemsSourcePropertyChanged)));

		private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
		    var control = sender as RectDiagramm;
		    if (control != null)
		        control.OnItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
		}	

		private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
		{
		    // Remove handler for oldValue.CollectionChanged
		    var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
		
		    if (null != oldValueINotifyCollectionChanged)
		    {
		        oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
		    }
		    // Add handler for newValue.CollectionChanged (if possible)
		    var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
		    if (null != newValueINotifyCollectionChanged)
		    {
		        newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
				grid.ColumnDefinitions.Clear();
				grid.Children.Clear();
				var cnt = 0;
				if( newValue is ObservableCollection<PositionViewModel> )
				{
					foreach(PositionViewModel p in newValue)
					{
						var col = new ColumnDefinition(){
							Width = new GridLength((double)p.Percent, GridUnitType.Star)
						};
						grid.ColumnDefinitions.Add(col);
						var btn = new Button();
						var bind = new Binding("Positions[" + cnt + "].Portfolio.Color");
						btn.BorderBrush = Brushes.Transparent;
						btn.SetBinding(Control.BackgroundProperty, bind);
						btn.ToolTip = string.Format("{0} - {1:F2}%", p.Portfolio.Name, p.Percent);
						
						Grid.SetColumn(btn, cnt++);
						grid.Children.Add(btn);
						btn.Click += (s, e) => {
							ActorSystem.Instance.Tell<MainActor>(new MsgFocus(p.Portfolio, p));
							Debug.WriteLine(p.Portfolio.Name + "; " + p.FullName);
						};
					}
					return;
				}

				Debug.WriteLine("Не знаю такого типа");
		    }
		}

		void newValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Debug.WriteLine("Поменялось");
			grid.ColumnDefinitions.Clear();			
		}
	}
}