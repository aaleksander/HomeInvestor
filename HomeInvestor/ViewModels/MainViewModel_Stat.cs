using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using HomeInvestor.Actors;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Все, что касается статистики
	/// </summary>
	public partial class MainViewModel
	{
		#region условия для статистики
		enum StatTypes {none, type, portfolio, all}
		StatTypes StatType{set;get;}
		object StatObject{set;get;}

		public bool StatByType{
			set{
				_statByType = value;
				OnPropertyChanged("StatByType");
				Statistica = null;
			}
			get{
				return _statByType;
			}
		}
		bool _statByType = false;

		public bool StatByPortfolio{
			set{
				_statByPortfolio = value;
				OnPropertyChanged("StatByPortfolio");
				Statistica = null;
			}
			get{
				return _statByPortfolio;
			}
		}
		bool _statByPortfolio = true;

		public bool StatByAll{
			set{
				_statByAll = value;
				OnPropertyChanged("StatByAll");
				Statistica = null;
			}
			get{
				return _statByAll;
			}
		}
		bool _statByAll = false;

		public bool History{
			set{
				_history = value;
				OnPropertyChanged("History");
			}
			get{
				return _history;
			}
		}
		bool _history = false;

		public decimal StatSum{
			set{
				_statSum = value;
				OnPropertyChanged("StatSum");
			}
			get{
				return _statSum;
			}
		}
		decimal _statSum = 0;

		public int StatCount{
			set{
				_statCount = value;
				OnPropertyChanged("StatCount");
			}
			get{
				return _statCount;
			}
		}
		int _statCount = 0;	

		#endregion

		StatisticaActor _statActor = new StatisticaActor();

		public SeriesCollection Statistica
		{
			get{
				Tuple<SeriesCollection, decimal> res = null;
				if( StatType == StatTypes.portfolio && StatObject == null ) //общая по портфелям
				{					
					res = StatAllPortfolio();
				}

				if( StatType == StatTypes.type && StatObject == null )	//общая по типам
				{
					res = StatAllTypes();
				}

				if( StatType == StatTypes.type && StatObject != null ) //по какому-то  одному типу
				{
					if( StatObject is string )
					{
						var t = (string)StatObject;
						switch(t)
						{
							case "Акции": res = GetStatByType(ActiveTypes.stocks); break;
							case "Облигации": res = GetStatByType(ActiveTypes.bonds); break;
							case "ETF": res = GetStatByType(ActiveTypes.etfs); break;
//							default: throw new Exception("ery");
						}						
					}
				}

				if( StatType == StatTypes.all )
				{
					if( StatObject == null )
						res = StatAll();
					else
						if( !(StatObject is string) )
							res = StatAllByType(StatObject);
				}

				if( res != null )
				{
					_statistica = res.Item1;
					StatSum = res.Item2;
					StatCount = res.Item1.Count;
				}
				
				return _statistica;
			}
			set{
				OnPropertyChanged("Statistica");
			}
		}
		SeriesCollection _statistica;

		void StatClick(object o)
		{
			var ser = (PieSeries)(o as ChartPoint).SeriesView;
			var t = ser.Tag;

			StatObject = t;
			OnPropertyChanged("Statistica");
		}

		Tuple<SeriesCollection, decimal> StatAllByType(object obj)
		{
			var res = new SeriesCollection();
			decimal sum = 0;
			var dict = new Dictionary<string, decimal>();

			foreach(var p in Portfolios)
			{
				foreach(var pos in p.Positions)
				{
					if( (obj is StockTypeViewModel) && pos.ActiveType == ActiveTypes.stocks )
					{
						if( (pos.Active as StockViewModel).StockType.Name == (obj as StockTypeViewModel).Name )
						{
							if( !dict.ContainsKey(pos.FullName ) )
								dict[pos.FullName] = 0L;
							dict[pos.FullName] += pos.TotalCost;
							sum += pos.TotalCost;
						}
					}
					
					if( (obj is BondTypeViewModel) && pos.ActiveType == ActiveTypes.bonds )
					{
						if( (pos.Active as BondViewModel).BondType.Name == (obj as BondTypeViewModel).Name )
						{
							if( !dict.ContainsKey(pos.FullName ) )
								dict[pos.FullName] = 0L;
							dict[pos.FullName] += pos.TotalCost;
							sum += pos.TotalCost;
						}
					}
					
					if( (obj is EtfTypeViewModel) && pos.ActiveType == ActiveTypes.etfs )
					{
						if( (pos.Active as EtfViewModel).EtfType.Name == (obj as EtfTypeViewModel).Name )
						{
							if( !dict.ContainsKey(pos.FullName ) )
								dict[pos.FullName] = 0L;
							dict[pos.FullName] += pos.TotalCost;
							sum += pos.TotalCost;
						}
					}
				}
			}

			var perc = StripPie(dict, 0);

			foreach(var k in perc.Keys)
			{
				var n = new PieSeries()
				        {
							Title = k,
							Values = new ChartValues<ObservableValue> { new ObservableValue((double)perc[k]) },
		                    DataLabels = true,
		                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.SeriesView.Title, chartPoint.Participation),
		                    FontSize = 16,
		                    Foreground = Brushes.Black,
		                    LabelPosition = PieLabelPosition.OutsideSlice,		                    
				};
				n.Stroke = Brushes.Black;
				n.StrokeThickness = 1;
				n.Tag = k;
				res.Add(n);				
			}
			
			return Tuple.Create(res, sum);
		}

		/// <summary>
		/// статистика по портфелям
		/// </summary>
		/// <returns></returns>
		Tuple<SeriesCollection, decimal> StatAllPortfolio()
		{
			var res = new SeriesCollection();
			decimal sum = 0;
			foreach(var p in Portfolios)
			{
				var n = new PieSeries()
				        {
             				Title = p.Name, 
             				Values = new ChartValues<ObservableValue> { new ObservableValue((double)p.TotalCost) },
		                    DataLabels = true,
		                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.SeriesView.Title, chartPoint.Participation),
		                    Fill = p.Color,
		                    FontSize = 20,
		                    Foreground = Brushes.Black,
		                    LabelPosition = PieLabelPosition.OutsideSlice		                    
				};
				n.Stroke = Brushes.Black;
				n.StrokeThickness = 1;
				n.Tag = p;
				
				res.Add(n);
				sum += p.TotalCost;
			}
			return Tuple.Create(res, sum);
		}

		/// <summary>
		/// собираем статистику по акциям/облигациям по всем портфелям
		/// </summary>
		/// <returns></returns>
		Tuple<SeriesCollection, decimal> GetStatByType(ActiveTypes t)
		{
			//собираем статистику по типам
			var dict = new Dictionary<string, decimal>();
			var res = new SeriesCollection();
			decimal sum = 0;

			foreach(var p in Portfolios)
			{
				foreach(var pos in p.Positions)
				{
					if( pos.ActiveType == t )
					{
						if( !dict.ContainsKey(pos.FullName ) )
							dict[pos.FullName] = 0L;
						dict[pos.FullName] += pos.TotalCost;
						sum += pos.TotalCost;
					}
				}
			}			 

			var perc = StripPie(dict, 0);

			foreach(var k in perc.Keys)
			{
				var n = new PieSeries()
				        {
							Title = k,
							Values = new ChartValues<ObservableValue> { new ObservableValue((double)perc[k]) },
		                    DataLabels = true,
		                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.SeriesView.Title, chartPoint.Participation),
		                    FontSize = 16,
		                    Foreground = Brushes.Black,
		                    LabelPosition = PieLabelPosition.OutsideSlice
				};
				n.Stroke = Brushes.Black;
				n.StrokeThickness = 1;
				n.Tag = k;
				res.Add(n);				
			}
			return Tuple.Create(res, sum);
		}

		/// <summary>
		/// убирает все сектора меньше minPie и объединяет их в один
		/// </summary>
		/// <param name="dict"></param>
		/// <param name="minPie"></param>
		/// <returns></returns>
		Dictionary<string, decimal> StripPie(Dictionary<string, decimal> dict, decimal minPie)
		{
			var sum = dict.Values.Sum();
			//сами посчитаем проценты и объединим маленькие сектора в один
			var perc = new Dictionary<string, decimal>();
			foreach(var k in dict.Keys)
				perc[k] = dict[k]/sum*100;

			var other = "остальное";
			perc[other] = 0;

			var p = perc.FirstOrDefault( pair => pair.Value <= minPie && pair.Key != other);
			while( p.Key != null )
			{
				Debug.WriteLine("Убираем из статистики: " + p.Key);
				perc[other] += p.Value;
				perc.Remove(p.Key);
				p = perc.FirstOrDefault( pair => pair.Value <= minPie && pair.Key != other);
			}

			if( perc[other] == 0 ) //ничего не смогли объединить
			{
				perc.Remove(other);
			}

			return perc;
		}

		/// <summary>
		/// все типы
		/// </summary>
		/// <returns></returns>
		Tuple<SeriesCollection, decimal> StatAll()
		{			
			//собираем все подтипы активов
			var dict = new Dictionary<object, decimal>();
			foreach(var t in UMDQuery.Select<StockTypeViewModel>())
				dict[t] = 0;

			foreach(var t in UMDQuery.Select<BondTypeViewModel>())
				dict[t] = 0;
			
			foreach(var t in UMDQuery.Select<EtfTypeViewModel>())
				dict[t] = 0;

			decimal sum = 0;
			
			foreach(var port in Portfolios)
			{
				foreach(var pos in port.Positions)
				{
					switch(pos.ActiveType)
					{
						case ActiveTypes.stocks:
							dict[(pos.Active as StockViewModel).StockType] += pos.TotalCost;
							break;
						case ActiveTypes.bonds:
							dict[(pos.Active as BondViewModel).BondType] += pos.TotalCost;
							break;
						case ActiveTypes.etfs:
							dict[(pos.Active as EtfViewModel).EtfType] += pos.TotalCost;
							break;
					}
					sum += pos.TotalCost;
				}
			}
			
			//удаляем нулевые
			while( dict.Any(x => x.Value == 0) )
			{
				var k = dict.First(x => x.Value == 0 );
				dict.Remove(k.Key);
			}
			
			var res = new SeriesCollection();
			foreach(var k in dict.Keys)
			{
				var n = new PieSeries()
				        {
							Title = k.ToString(),
							Values = new ChartValues<ObservableValue> { new ObservableValue((double)dict[k]) },
		                    DataLabels = true,
		                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.SeriesView.Title, chartPoint.Participation),
		                    FontSize = 20,
		                    Foreground = Brushes.Black,
		                    LabelPosition = PieLabelPosition.OutsideSlice
				};
				n.Stroke = Brushes.Black;
				n.StrokeThickness = 1;
				n.Tag = k;
				res.Add(n);
			}
			
			return Tuple.Create(res, sum);
		}

		
		string ActiveType2String(ActiveTypes a)
		{
			switch(a)
			{
				case ActiveTypes.bonds: 	return "Облигации";
				case ActiveTypes.stocks: 	return "Акции";
				case ActiveTypes.etfs: 		return "ETF";
				default: throw new Exception("!");
			}			
		}

		Tuple<SeriesCollection, decimal> StatAllTypes()
		{
			//собираем статистику по типам
			var dict = new Dictionary<string, decimal>();

			foreach(var p in Portfolios)
			{
				foreach(var pos in p.Positions)
				{
					var k = ActiveType2String(pos.ActiveType);
					if( !dict.ContainsKey(k) )
						dict[k] = 0L;
						
					dict[k] += pos.TotalCost;
				}
			}

			var res = new SeriesCollection();
			decimal sum = 0;
			foreach(var k in dict.Keys)
			{
				var n = new PieSeries()
				        {
							Title = k.ToString(),
							Values = new ChartValues<ObservableValue> { new ObservableValue((double)dict[k]) },
		                    DataLabels = true,
		                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.SeriesView.Title, chartPoint.Participation),
		                    FontSize = 20,
		                    Foreground = Brushes.Black,
		                    LabelPosition = PieLabelPosition.OutsideSlice
				};
				n.Stroke = Brushes.Black;
				n.StrokeThickness = 1;
				n.Tag = k;
				res.Add(n);
				sum += dict[k];
			}
			return Tuple.Create(res, sum);
		}

		/// <summary>
		/// статистика по типам по одному портфелю
		/// </summary>
		/// <param name="port"></param>
		/// <returns></returns>
		Tuple<SeriesCollection, decimal> StatPortfolio(PortfolioViewModel port)
		{
			//собираем статистику по типам по одному портфелю
			var dict = new Dictionary<ActiveTypes, decimal>();

			foreach(var pos in port.Positions)
			{
				if( !dict.ContainsKey(pos.ActiveType) )
					dict[pos.ActiveType] = 0L;

				dict[pos.ActiveType] += pos.TotalCost;
			}

			var res = new SeriesCollection();
			decimal sum = 0;
			foreach(var k in dict.Keys)
			{
				var n = new PieSeries()
				        {
							Title = k.ToString(),
							Values = new ChartValues<ObservableValue> { new ObservableValue((double)dict[k]) },
		                    DataLabels = true,
		                    LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.SeriesView.Title, chartPoint.Participation),
		                    FontSize = 20,
		                    Foreground = Brushes.Black,
		                    LabelPosition = PieLabelPosition.OutsideSlice
				};
				n.Stroke = Brushes.Black;
				n.StrokeThickness = 1;
				n.Tag = k;
				res.Add(n);
				sum += dict[k];
			}
			return Tuple.Create(res, sum);
		}
	}
}
