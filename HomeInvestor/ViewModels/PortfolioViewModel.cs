using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using HomeInvestor.Actors;
using HomeInvestor.Dlg;
using HomeInvestor.Dlg.AddActiveWizard;
using HomeInvestor.Pages;
using UMD;

namespace HomeInvestor.ViewModels
{
	[Alias("portfolio")]
	[Properties(
		"str", "decsription",
		"str", "color"
	)]
	/// <summary>
	/// портфель
	/// </summary>
	public class PortfolioViewModel: UMDViewModelBase
	{
		public PortfolioViewModel(IStorage st): base(st)
		{
			Init();
		}

        public PortfolioViewModel(IStorage aSt, UMDObjectModel aModel) : base(aSt, aModel)
        {
        	Init();
        }

        PortfolioActor _actor;

        public RelayCommand SettingsCommand{private set;get;}
		public RelayCommand ShowPlanCommand{private set;get;}
		public RelayCommand RemoveCommand{private set;get;}
		public RelayCommand ShowFutureCommand{private set;get;}
		public RelayCommand ChangeColorCommand{private set;get;}
		public RelayCommand AddCommand{private set;get;}
		public RelayCommand RemovePlanCommand{private set;get;}
		public RelayCommand AddMoneyCommand{private set;get;}
		public RelayCommand NavPortfolioCommand{private set;get;}

        void Init()
        {
        	RemoveCommand = new RelayCommand(o => {
        	                                 	if( MessageBox.Show("Удаляем портфель " + Name + "\nУверены?", "Внимание", 
        	                                 	                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes )
        	                                 	{
        	                                 		//сообщаем верху и низу, что удаляется портфель
        	                                 		ActorSystem.Instance[names.mainWindow].Tell(new MsgRemovePortfolio(this));
        	                                 		ActorSystem.Instance[names.mainVM].Tell(new MsgRemovePortfolio(this));
        	                                 		Main.Navigate<HomePage>(Main);
        	                                 	}
        	                                 });

        	SettingsCommand = new RelayCommand(o => { //настройка портфеля
        	                                   	var dlg = new dlgPortfolioSettings();
        	                                   	dlg.DataContext = this;
        	                                   	dlg.ShowDialog();
        	                                   });

        	ChangeColorCommand = new RelayCommand(o => { //изменение цвета портфеля
        	                                      	this.Color = Helpers.Colors.GetColor(this.Color);
        	                                      });

        	AddCommand = new RelayCommand(AddActive);

			NavPortfolioCommand = new RelayCommand(o => Main.Navigate<PortfolioPage>(this));
        	_actor = new PortfolioActor("portfolio_" + GetHashCode(), this);

        	_position = UMDQuery.Select<PositionViewModel>().Where(a => a.ParentId == this.Id).ToOC();
        }

        #region поля
		public string Description
		{
			set
			{
				if( GetStr(0) == value )
					return;
				SetProp(0, value);
				UpdateIfNeed();
				OnPropertyChanged("Description");
			}
			get
			{
				return GetStr(0);
			}
		}

		public SolidColorBrush Color
		{
			set
			{
				SetProp(1, value);
				UpdateIfNeed();
				OnPropertyChanged("Color");
//				if( IsPlan == false && Id > 0 ) //у планового портфеля тоже меняем
//				{
//					Plan.Color = value;
//				}
			}
			get
			{
				if( RealTimeUpdate == false ) //это общий портфель
					return Brushes.White;
				return GetColor(1);
			}
		}
        #endregion

        public decimal TotalCost
        {
        	get{
        		if( _totalCost <  0 )
        		{
	        		decimal res = 0M;
	        		foreach(var a in Positions)
	        		{
	        			res += a.TotalCost;
	        		}
	        		_totalCost = res;
        		}
        		return _totalCost;
        	}
        	set{
        		_totalCost = value;
        		OnPropertyChanged("TotalCost");
        	}
        }
        decimal _totalCost = -1;

		public MainViewModel Main{ 
        	set{
        		_main = value;
        		OnPropertyChanged("Main");
        	}
        	get{
        		return _main;
        	}
        }
        MainViewModel _main = null;

        public ObservableCollection<PositionViewModel> Positions{
        	get{
        		return _position;
        	}
        }
        ObservableCollection<PositionViewModel> _position = null;

        public void UpdatePercents()
        {
        	TotalCost = -1;
        	var ttt = TotalCost;
        	foreach(var p in Positions)
        		p.Percent = 0;
//        	Main.UpdatePercents(); //для глобального портфеля
        }

        #region команды
        void AddActive(object o)
        {
        	var dlg = new dlgSelectActive();
        	var res = dlg.ShowDialog();

        	if( !res.HasValue || !res.Value )
        		return;

        	int? cnt;
        	PositionViewModel pos = null;
        	switch(dlg.Selected)
        	{
        		case 1:
        			var st = new dlgSelectStock();
        			st.DataContext = Main;
        			res = st.ShowDialog();
        			if( !res.HasValue || !res.Value)
        				return;
        			cnt = InputBox.GetInt("Шаг 3", "Количество акций:", 0);
        			if( cnt.HasValue && cnt.Value > 0 )
        			{
        				var tmp = (StockViewModel)st.grid.SelectedItem;

        				//проверить, чтобы такой акции еще не было.
        				foreach(var p in Positions)
        				{
        					if( p.ActiveId == tmp.Id )
        					{
        						MessageBox.Show("Такой актив уже есть, измените у него объем");
        						return;
        					}
        				}

        				pos = new PositionViewModel(_storage)
        				{
        					ParentId = this.Id,
        					ActiveType = ActiveTypes.stocks, //акции
        					ActiveId = tmp.Id,
        					Volume = cnt.Value
        				};
        			}
        			break;
        		case 2:
        			var ob = new dlgSelectBond();
        			ob.DataContext = Main;
        			res = ob.ShowDialog();
        			if( !res.HasValue || !res.Value)
        				return;
        			cnt = InputBox.GetInt("Шаг 3", "Количество облигаций:", 0);
        			if( cnt.HasValue && cnt.Value > 0 )
        			{
        				var tmp = (BondViewModel)ob.grid.SelectedItem;

        				//проверить, чтобы такой акции еще не было.
        				foreach(var p in Positions)
        				{
        					if( p.ActiveId == tmp.Id )
        					{
        						MessageBox.Show("Такой актив уже есть, измените у него объем");
        						return;
        					}
        				}
        				pos = new PositionViewModel(_storage)
        				{
	        				ParentId = this.Id,
	        				ActiveType = ActiveTypes.bonds,
	        				ActiveId = tmp.Id,
	        				Volume = cnt.Value
        				};
        			}
        			break;
        		case 3:
        			var et = new dlgSelectEtf();
        			et.DataContext = Main;
        			res = et.ShowDialog();
        			if( !res.HasValue || !res.Value)
        				return;
        			cnt = InputBox.GetInt("Шаг 3", "Количество облигаций:", 0);
        			if( cnt.HasValue && cnt.Value > 0 )
        			{
        				var tmp = (EtfViewModel)et.grid.SelectedItem;
        				//проверить, чтобы такой ETF еще не было в портфеле
        				foreach(var p in Positions)
        				{
        					if( p.ActiveId == tmp.Id )
        					{
        						MessageBox.Show("Такой актив уже есть, измените у него объем");
        						return;
        					}
        				}
        				pos = new PositionViewModel(_storage)
        				{
	        				ParentId = this.Id,
	        				ActiveType = ActiveTypes.etfs,
	        				ActiveId = tmp.Id,
	        				Volume = cnt.Value
        				};
        			}
        			break;
        	}

        	if( pos != null )
        	{
				pos.Insert();
				Positions.Add(pos);
				TotalCost = -1;
				UpdatePercents();
        	}
        }
        #endregion
	}
}

//TODO когда меняю тип акции/облигации/etf, в портфеле у позиции этот тип не меняется (надо прокидывать актора или сообщение)

