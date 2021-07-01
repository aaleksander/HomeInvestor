using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using HomeInvestor.Actors;
using HomeInvestor.Dlg;
using UMD;

namespace HomeInvestor.ViewModels
{
	public class MsgUpdateVolume
	{
		public MsgUpdateVolume(PositionViewModel pos)
		{
			Positon = pos;
		}
	
		public PositionViewModel Positon{set;get;}
	}

	public enum ActiveTypes {none=0, stocks, bonds, etfs}
	/// <summary>
	/// Description of PositionViewModel.
	/// </summary>
	[Alias("Position")]
	[Properties(
		"int", "type",
		"int", "volume",
		"int", "actId",
		"dec", "avgPrice"
	)]
	public class PositionViewModel: UMDViewModelBase
	{
		public PositionViewModel(IStorage _st): base(_st)
		{
			Init();
		}

		public PositionViewModel(IStorage st, UMDObjectModel m): base(st, m)
		{
			Init();
		}

		public MainViewModel Main{set;get;}

		void Init()
		{
			Main = null;
			_actor = new PositionActor("pos_" + GetHashCode() + "_" + ActiveHashCode.ToString(), Active);

			_actor.OnChangePrice += o => {
				if( Portfolio == null ) //для позиций, которые "потерялись"
					return;
				Debug.WriteLine(Portfolio.Name + "; " + FullName +  "; OnChangePrice");
				OnPropertyChanged("Price");
				TotalCost = -1;
				Portfolio.UpdatePercents();
			};

			RemoveCommand = new RelayCommand( o => 
			                                 {
			                                 	//TODO нужно переделать так, чтобы позиция просто отмечалась как удаленная. 
			                                 	//в портфеле бы не показывалась, но отражалась бы в истории
			                                 	//это позволит построить всю историю

			                                 	_storage.BeginTransaction();
			                                 	//удаляем историю позиции
			                                 	foreach(var h in History)
			                                 		h.Delete();

			                                 	Portfolio.Positions.Remove(this);
			                                 	this.Delete();
			                                 	_storage.Commit();
			                                 	Portfolio.TotalCost = -1;
			                                 	Portfolio.UpdatePercents();
			                                 });

			HistoryCommand = new RelayCommand(o =>
			                                  {
			                                  	var dlg = new dlgHistory(){
			                                  		DataContext = this
			                                  	};
			                                  	dlg.ShowDialog();
			                                  	//пересчитать CheckHistory
			                                  	CheckHistory = false;
			                                  });

			AddHistoryCommand = new RelayCommand(o => 
			                                     {
			                                     	var nh = new HistoryViewModel(_storage)
			                                     	{
			                                     		ParentId = this.Id
			                                     	};
			                                     	nh.Insert();
			                                     	History.Add(nh);
			                                     });
		}

		public RelayCommand RemoveCommand{private set;get;}
		public RelayCommand HistoryCommand{private set;get;}
		public RelayCommand AddHistoryCommand{private set;get;}

		PositionActor _actor;

		#region поля
		/// <summary>
		/// тип актива (акция или облигация)
		/// </summary>
		public ActiveTypes ActiveType{
			set{
				SetProp(0, (int)value);
				OnPropertyChanged("ActiveType");
			}
			get{
				return (ActiveTypes) GetInt(0);
			}
		}

		/// <summary>
		/// объем позиции в штуках
		/// </summary>
		public int Volume{
			set{
				//TODO когда меняю объем позиции надо предложить добавить в историю запись
				SetProp(1, value);
				TotalCost = -1;
				var tt = TotalCost;
				OnPropertyChanged("Volume");
				OnPropertyChanged("CheckHistory");
				OnPropertyChanged("CheckHistoryBrush");

				
				if( Portfolio != null )
				{
					Portfolio.UpdatePercents();
				}
				new Thread(() => {
				           	Thread.Sleep(500);
							ActorSystem.Instance.Tell<StatisticaActor>(new MsgUpdateVolume(this));
				           }).Start();
			}
			get{
				return GetInt(1);
			}
		}

		/// <summary>
		/// идентификатор акции или облигации
		/// </summary>
		public int ActiveId{
			set{
				SetProp(2, value);
				OnPropertyChanged("ActiveId");
				OnPropertyChanged("Active");
			}
			get{
				return GetInt(2);
			}
		}

		/// <summary>
		/// средняя цена покупки
		/// </summary>
		public decimal AvgPrice{
			set{
				SetProp(3, value);
				FinRez = 0;
				OnPropertyChanged("AvgPrice");
			}
			get{
				return GetDecimal(3);
			}
		}		
		#endregion

		public string FullName{
			get{
				switch( ActiveType )
				{
					case ActiveTypes.stocks:
						var o = Active as StockViewModel;// UMDQuery.ById<StockViewModel>(ActiveId);
						return o.FullName;
					case ActiveTypes.bonds:
						var ob = Active as BondViewModel;// UMDQuery.ById<BondViewModel>(ActiveId);
						return ob.FullName;
					case ActiveTypes.etfs:
						var et = Active as EtfViewModel;// UMDQuery.ById<BondViewModel>(ActiveId);
						return et.FullName;
					default:
						throw new Exception("dd");
				}
			}
		}

		public decimal Price{
			get{
				switch( ActiveType )
				{
					case ActiveTypes.stocks:
						var o = Active as StockViewModel;// UMDQuery.ById<StockViewModel>(ActiveId);
						return o.Price;
					case ActiveTypes.bonds:
						var ob = Active as BondViewModel; //  UMDQuery.ById<BondViewModel>(ActiveId);
						return ob.Price;
					case ActiveTypes.etfs:
						var et = Active as EtfViewModel;// UMDQuery.ById<StockViewModel>(ActiveId);
						return et.Price;
					default:
						throw new Exception("dd");
				}
			}
		}

		public decimal TotalCost{
			get{
				if( _totalCost <= 0 )
				{
					switch( ActiveType )
					{
						case ActiveTypes.stocks:
						case ActiveTypes.etfs:
							_totalCost = Price*Volume;
							break;
						case ActiveTypes.bonds:
							var ob = Active as BondViewModel;
							_totalCost = Price/100*ob.NominalNow*Volume + ob.NCD*Volume;
							break;
						default:
							throw new Exception("dd");
					}
				}
				return _totalCost;
			}
			set{
				_totalCost = value;
				OnPropertyChanged("TotalCost");
			}
		}
		decimal _totalCost = -1;

		public object Active{
			get{
				if( _active == null )
				{
					switch( ActiveType )
					{
						case ActiveTypes.stocks:
							_active = UMDQuery.First<StockViewModel>(x => x.Id == ActiveId);
							break;
						case ActiveTypes.bonds:
							_active = UMDQuery.First<BondViewModel>(x => x.Id == ActiveId);
							break;
						case ActiveTypes.etfs:
							_active = UMDQuery.First<EtfViewModel>(x => x.Id == ActiveId);
							break;
					}
				}
				return _active;
			}
		}
		object _active = null;

		public int ActiveHashCode{
			get{
				if( _activeHashCode == 0 && Active != null )
				{
					_activeHashCode = Active.GetHashCode();
				}
				return _activeHashCode;
			}
		}
		int _activeHashCode = 0;

		public PortfolioViewModel Portfolio{
			get{
				if( _portfolio == null && ParentId != -1 ) //-1 - это общий портфель
				{
					_portfolio = UMDQuery.First<PortfolioViewModel>(x => x.Id == ParentId);
				}
				return _portfolio;
			}
		}
		PortfolioViewModel _portfolio = null;

		public decimal Percent{
			get{
				if( Portfolio != null )
					return TotalCost/Portfolio.TotalCost*100;
				else
					return TotalCost/Main.TotalCost*100;
			}
			set{
				OnPropertyChanged("Percent");
			}
		}

		/// <summary>
		/// это для общего портфеля
		/// </summary>
		public ObservableCollection<PositionViewModel> Positions{set; get;}

		/// <summary>
		/// финансовый результат
		/// </summary>
		public decimal FinRez
		{
			get{
				switch(ActiveType)
				{
					case ActiveTypes.etfs: 
					case ActiveTypes.stocks: 
						return TotalCost - Volume*AvgPrice;
					case ActiveTypes.bonds:
						var ob = Active as BondViewModel;
						return TotalCost - AvgPrice/100*ob.NominalNow*Volume;
				}
				return 0;
			}
			set{
				OnPropertyChanged("FinRez");
			}
		}

		public string TypeName{
			get{
				switch( ActiveType )
				{
					case ActiveTypes.stocks:
						var o = UMDQuery.ById<StockViewModel>(ActiveId);
						if( o.StockType != null )
						{
							var tn = o.StockType.Name;
							return "А, " + tn;
						}
						else
							return "А";

					case ActiveTypes.bonds:
						var ob = UMDQuery.ById<BondViewModel>(ActiveId);
						if( ob.BondType != null )
						{
							var tn = ob.BondType.Name;
							return "О, " + tn;
						}
						else
							return "О";

					case ActiveTypes.etfs:
						var et = UMDQuery.ById<EtfViewModel>(ActiveId);
						if( et.EtfType != null )
						{
							var tn = et.EtfType.Name;
							return "E, " + tn;
						}
						else
							return "E";
					default:
						throw new Exception("dd");
				}
			}
		}

		public string OtraslName{
			get{
				switch( ActiveType )
				{
					case ActiveTypes.stocks:
						var o = UMDQuery.ById<StockViewModel>(ActiveId);
						if( o.Otrasl != null )
						{
							var tn = o.Otrasl.Name;
							return tn;
						}
						break;
					case ActiveTypes.bonds:
						break;						
					case ActiveTypes.etfs:
						break;
					default:
						throw new Exception("dd");
				}
				return "";
			}
		}

		/// <summary>
		/// доходность к погашению, исходя из цены покупки
		/// </summary>
		public decimal? CurrentDohod{
			get{
				if( ActiveType != ActiveTypes.bonds ) //только для бондов
					return null;

				//ближайший купон
				var bond = Active as BondViewModel;

				var cup =  bond.Cupons.FirstOrDefault(x => x.First);
				if( cup == null )
					return -1;
				if( bond.Cupons.Count < 2 )
					return -1;

				//определяем сколько раз в год платится
				var days = (bond.Cupons[1].DT - bond.Cupons[0].DT).TotalDays;
				var m = (decimal)Math.Round(365/days);
				Debug.WriteLine(days + ", " + m);

				var price = (AvgPrice*bond.NominalNow/100); //средняя цена
				var currDoh = m*cup.Size/price*100.0M;
				return currDoh + (bond.NominalNow - price)/price*365/bond.DaysLeftPogash*100.0M;
			}
		}

		/// <summary>
		/// доходность к погашению, исходя из текущей цены
		/// </summary>
		public decimal? Dohod{
			get{
				if( ActiveType != ActiveTypes.bonds ) //только для бондов
					return null;

				//ближайший купон
				var bond = Active as BondViewModel;

				var cup =  bond.Cupons.FirstOrDefault(x => x.First);
				if( cup == null )
					return -1;
				if( bond.Cupons.Count < 2 )
					return -1;

				//определяем сколько раз в год платится
				var days = (bond.Cupons[1].DT - bond.Cupons[0].DT).TotalDays;
				var m = (decimal)Math.Round(365/days);
				Debug.WriteLine(days + ", " + m);

				var price = (Price*bond.NominalNow/100); //средняя цена
				var currDoh = m*cup.Size/price*100.0M;
				return currDoh + (bond.NominalNow - price)/price*365/bond.DaysLeftPogash*100.0M;
			}
		}

		public ObservableCollection<HistoryViewModel> History{
			get{
				if( _history == null )
				{
					_history = UMDQuery.Select<HistoryViewModel>().Where(x => x.ParentId == Id).OrderBy(x => x.DT).ToOC();
				}
				return _history;
			}
		}
		ObservableCollection<HistoryViewModel> _history = null;

		/// <summary>
		/// объем позиции, судя по истории
		/// </summary>
		public int HistoryVolume{
			get{
				var res = 0;

				foreach(var h in History)
				{
					switch(h.Operation)
					{
						case "buy":  res += h.Volume; break;
						case "sell": res -= h.Volume; break;
						default: throw new Exception("Неизвестная операция в истории: " + h.Operation);
					}
				}
				return res;
			}
		}

//TODO среднюю цену покупки расчитывать исходя из истории
//TODO див.доходность считать по средней цене покупки

		public bool CheckHistory{
			get{
				return Volume == HistoryVolume;
			}
			set{
				OnPropertyChanged("CheckHistory");
				OnPropertyChanged("CheckHistoryBrush");
			}
		}

		public Brush CheckHistoryBrush
		{//FIXME лютый костыль. Почему-то через конвертер не работает
			get{
				return CheckHistory? Brushes.Green: Brushes.Red;
			}
		}
	}
}


