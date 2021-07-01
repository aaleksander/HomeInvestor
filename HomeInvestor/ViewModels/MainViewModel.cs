using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using HomeInvestor.Actors;
using HomeInvestor.Dlg;
using HomeInvestor.Pages;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of MainViewModel.
	/// </summary>
	public partial class MainViewModel: INotifyPropertyChanged
	{
		public MainViewModel()
		{
			Init();
		}

		IStorage _storage;
		MainActor _actor;

		public RelayCommand AddPortfolioCommand{private set;get;}
		public RelayCommand ShowOblDictCommand{private set;get;}
		public RelayCommand UpdatePositionsCommand{private set;get;}

		public RelayCommand NavHomeCommand{private set;get;}
		public RelayCommand NavCurrencyDictCommand{private set;get;}
		public RelayCommand NavEtfDictCommand{private set;get;}
		public RelayCommand NavStocksDictCommand{private set;get;}
		public RelayCommand NavBondsDictCommand{private set;get;}
		public RelayCommand NavCommonPortfolioCommand{private set;get;}
		public RelayCommand NavVDOCommand{private set;get;}

		public RelayCommand AddStockCommand{private set;get;}
		public RelayCommand EditStockCommand{private set;get;}
		public RelayCommand RemoveStockCommand{private set;get;}
		public RelayCommand AddStockTypeCommand{private set;get;}
		public RelayCommand RemoveStockTypeCommand{private set;get;}
		public RelayCommand AddOtraslCommand{private set;get;}
		public RelayCommand RemoveOtraslCommand{private set;get;}
		

		public RelayCommand AddBondCommand{private set;get;}
		public RelayCommand EditBondCommand{private set;get;}
		public RelayCommand RemoveBondCommand{private set;get;}
		public RelayCommand AddBondTypeCommand{private set;get;}
		public RelayCommand RemoveBondTypeCommand{private set;get;}

		public RelayCommand AddEtfCommand{private set;get;}
		public RelayCommand EditEtfCommand{private set;get;}
		public RelayCommand RemoveEtfCommand{private set;get;}
		public RelayCommand AddEtfTypeCommand{private set;get;}
		public RelayCommand RemoveEtfTypeCommand{private set;get;}

		public RelayCommand StatByTypeCommand{private set;get;}
		public RelayCommand StatByPortfolioCommand{private set;get;}
		public RelayCommand StatByAllCommand{private set;get;}
		public RelayCommand StatClickCommand{private set;get;}

		public RelayCommand AddCuponCommand{private set;get;}
		public RelayCommand AddAmortCommand{private set;get;}
		public RelayCommand RemoveCuponCommand{private set;get;}
		public RelayCommand RemoveAmortCommand{private set;get;}
		
		public RelayCommand UpdateConditionsCommand{private set;get;}
		
		public RelayCommand HoverCommand{private set;get;}
		public RelayCommand ShowCuponsCommand{private set;get;}

		void Init()
		{
			_storage = (ActorSystem.Instance[names.loader] as DBLoader).Storage;
			_actor = new MainActor(this);

			_actor.OnFocus += (port, pos) => {
				Navigate<PortfolioPage>(port);

				//определяем позицию
				var ind = 0;
				foreach(var p in port.Positions)
				{
					if( p.ActiveId == pos.ActiveId )
						break;
					ind++;
				}
				(CurrentPage as PortfolioPage).grid.SelectedIndex = ind;
				(CurrentPage as PortfolioPage).grid.Focus();
			};

			AddPortfolioCommand = new RelayCommand(o => 
			                                       {
			                                       		//создаем портфель
														var p = new PortfolioViewModel(_storage)
														{
															Name = "новый",
															Color = Brushes.White,
															Main = this,
														};
														//отправляем сообщение нашему окну, что есть новый портфель
														ActorSystem.Instance[names.mainWindow].Tell(new MsgNewPortfolio(p));
														//параллельно завносим в БД
														_actor.Tell(new MsgNewPortfolio(p));
													});

			UpdateConditionsCommand = new RelayCommand(UpdateConditions);

			#region справочник облигаций
			AddBondTypeCommand = new RelayCommand( o => 
												{
			                                       	var n = new BondTypeViewModel(_storage);
			                                       	n.Name = InputBox.GetString("Новый тип облигаций", "Введите тип облигации:", "default");
			                                       	if( n.Name == "" )
			                                       		return;
			                                       	//проверить, чтобы такого имени еще не было
			                                       	if( BondTypes.Any(x => x.Name == n.Name ) )
			                                       	{
			                                       		MessageBox.Show("Такой тип уже существует");
			                                       		return;
			                                       	}
			                                       	n.Insert();
			                                       	BondTypes.Add(n);
			                                    });
			RemoveBondTypeCommand = new RelayCommand( o => 
			                                          {
			                                          	var st = (BondTypeViewModel) o;
			                                          	//проверить, чтобы такой тип нигде не использовался
			                                          	var s = Bonds.FirstOrDefault(x => x.BondType == st);
			                                          	if( s != null )
			                                          	{
			                                          		MessageBox.Show("Данные тип используется у облигации \"" + s.FullName + "\"\nУдаление невозможно");
			                                          		return;
			                                          	}
			                                          	BondTypes.Remove(st);
			                                          	st.Delete();
			                                          }, o => o != null);
			AddBondCommand = new RelayCommand( o => 
			                                   {
			                                   		//создать акцию
			                                   		var b = new BondViewModel(_storage){
			                                   			Main = this
			                                   		};
			                                   		var dlg = new dlgBond()
			                                   		{
			                                   			DataContext = b
			                                   		};
			                                   		var res = dlg.ShowDialog();
			                                   		if( !res.HasValue || !res.Value )
			                                   			return;
			                                   		//заводим облигацию в БД
			                                   		b.Insert();
			                                   		Bonds.Add(b);
			                                   });
			EditBondCommand = new RelayCommand( o => 
			                                   {
			                                   		var b = (BondViewModel)o;
			                                   		var dlg = new dlgBond()
			                                   		{
			                                   			DataContext = b
			                                   		};
			                                   		var res = dlg.ShowDialog();
			                                   		if( !res.HasValue || !res.Value )
			                                   			return;
			                                   }, o => o != null);
			RemoveBondCommand = new RelayCommand( o => 
			                                     {
			                                     	var oo = o as BondViewModel;
			                                     	//проверить, чтобы этой облигации не было в портфелях
			                                     	foreach(var p in Portfolios)
			                                     	{
			                                     		foreach(var pos in p.Positions)
			                                     		{
			                                     			if( pos.ActiveId == oo.Id )
			                                     			{
			                                     				MessageBox.Show("Такая облигация есть в портфеле '" + p.Name + "'");
			                                     				return;
			                                     			}
			                                     		}
			                                     	}
													if( MessageBox.Show("Удаляем облигацию " + oo.FullName + "?", "Внимание",
			                                      	                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes )
			                                      		return;
			                                     	_storage.BeginTransaction();
			                                     	oo.Delete();
			                                     	_storage.Commit();
			                                     	Bonds.Remove(oo);
			                                     }, o => o != null);
			#endregion

			#region справочник акций			
			AddStockCommand = new RelayCommand( o => 
			                                   {
			                                   		//создать акцию
			                                   		var s = new StockViewModel(_storage){
			                                   			Main = this
			                                   		};
			                                   		var dlg = new dlgStock()
			                                   		{
			                                   			DataContext = s
			                                   		};
			                                   		var res = dlg.ShowDialog();
			                                   		if( !res.HasValue || !res.Value )
			                                   			return;
			                                   		//заводим акцию в БД
			                                   		s.Insert();
			                                   		Stocks.Add(s);
			                                   });
			AddStockTypeCommand = new RelayCommand( o => 
												{
			                                       	var n = new StockTypeViewModel(_storage);
			                                       	n.Name = InputBox.GetString("Новый тип акций", "Введите тип акции:", "default");
			                                       	if( n.Name == "" )
			                                       		return;
			                                       	//проверить, чтобы такого имени еще не было
			                                       	if( StockTypes.Any(x => x.Name == n.Name) )
			                                       	{
			                                       		MessageBox.Show("Такой тип уже есть");
			                                       		return;
			                                       	}
			                                       	_storage.BeginTransaction();
			                                       	n.Insert();
			                                       	_storage.Commit();
			                                       	StockTypes.Add(n);
			                                    });
			
			AddOtraslCommand = new RelayCommand( o => 
												{
			                                       	var n = new OtraslViewModel(_storage);
			                                       	n.Name = InputBox.GetString("Новая отрасль", "Введите отрасль:", "default");
			                                       	if( n.Name == "" )
			                                       		return;
			                                       	//проверить, чтобы такого имени еще не было
			                                       	if( Otrasls.Any(x => x.Name == n.Name ) )
			                                       	{
			                                       		MessageBox.Show("Такая отрасль уже есть");
			                                       		return;
			                                       	}
			                                       	n.Insert();
			                                       	Otrasls.Add(n);
			                                    });

			EditStockCommand = new RelayCommand( o => 
			                                   {
			                                   		//создать акцию
			                                   		var s = (StockViewModel)o;
			                                   		var dlg = new dlgStock()
			                                   		{
			                                   			DataContext = s
			                                   		};
			                                   		var res = dlg.ShowDialog();
			                                   		if( !res.HasValue || !res.Value )
			                                   			return;
			                                   }, o => o != null);

			RemoveStockCommand = new RelayCommand( o =>
			                                      {
			                                      	var st = (StockViewModel)o;
													//проверить, чтобы этой облигации не было в портфелях
			                                     	foreach(var p in Portfolios)
			                                     	{
			                                     		foreach(var pos in p.Positions)
			                                     		{
			                                     			if( pos.ActiveId == st.Id )
			                                     			{
			                                     				MessageBox.Show("Такая облигация есть в портфеле '" + p.Name + "'");
			                                     				return;
			                                     			}
			                                     		}
			                                     	}
			                                      	if( MessageBox.Show("Удаляем акцию " + st.FullName + "?", "Внимание",
			                                      	                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes )
			                                      		return;
			                                      	//удаляем акцию из спровочника			                                      	
			                                      	_storage.BeginTransaction();
			                                      	st.Delete();
			                                      	_storage.Commit();
			                                      	Stocks.Remove(st);			                                      	
			                                      }, o => o != null);

			RemoveStockTypeCommand = new RelayCommand( o => 
			                                          {
			                                          	var st = (StockTypeViewModel) o;
			                                          	//проверить, чтобы такой тип нигде не использовался
			                                          	var s = Stocks.FirstOrDefault(x => x.StockType == st);
			                                          	if( s != null )
			                                          	{
			                                          		MessageBox.Show("Данные тип используется у акции \"" + s.FullName + "\"\nУдаление невозможно");
			                                          		return;
			                                          	}
			                                          	StockTypes.Remove(st);
			                                          	st.Delete();
			                                          }, o => o != null);

			RemoveOtraslCommand = new RelayCommand( o => 
			                                          {
			                                          	var st = (OtraslViewModel) o;
			                                          	//проверить, чтобы такой тип нигде не использовался
			                                          	var s = Stocks.FirstOrDefault(x => x.Otrasl == st);
			                                          	if( s != null )
			                                          	{
			                                          		MessageBox.Show("Данная отрасль используется у акции \"" + s.FullName + "\"\nУдаление невозможно");
			                                          		return;
			                                          	}
			                                          	Otrasls.Remove(st);
			                                          	st.Delete();
			                                          }, o => o != null);
			#endregion

			#region справочник ETF
			AddEtfCommand = new RelayCommand( o => 
			                                   {
			                                   		//создать акцию
			                                   		var s = new EtfViewModel(_storage){
			                                   			Main = this
			                                   		};
			                                   		var dlg = new dlgEtf()
			                                   		{
			                                   			DataContext = s
			                                   		};
			                                   		var res = dlg.ShowDialog();
			                                   		if( !res.HasValue || !res.Value )
			                                   			return;
			                                   		//заводим акцию в БД
			                                   		s.Insert();
			                                   		Etfs.Add(s);
			                                   });
			AddEtfTypeCommand = new RelayCommand( o => 
												{
			                                       	var n = new EtfTypeViewModel(_storage);
			                                       	n.Name = InputBox.GetString("Новый тип ETF", "Введите тип ETF:", "default");
			                                       	if( n.Name == "" )
			                                       		return;
			                                       	//проверяем, чтобы такого имени еще не было
			                                       	if( EtfTypes.Any(x => x.Name == n.Name) )
			                                       	{
			                                       		MessageBox.Show("Такой тип ETF уже есть");
			                                       		return;
			                                       	}			                                       	
			                                       	n.Insert();
			                                       	EtfTypes.Add(n);
			                                    });

			EditEtfCommand = new RelayCommand( o => 
			                                   {
			                                   		var s = (EtfViewModel)o;
			                                   		var dlg = new dlgEtf()
			                                   		{
			                                   			DataContext = s
			                                   		};
			                                   		var res = dlg.ShowDialog();
			                                   		if( !res.HasValue || !res.Value )
			                                   			return;
			                                   }, o => o != null);

			RemoveEtfCommand = new RelayCommand( o =>
			                                      {
			                                      	var st = (EtfViewModel)o;
			                                      	if( MessageBox.Show("Удаляем ETF " + st.FullName + "?", "Внимание",
			                                      	                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes )
			                                      		return;
			                                      	//удаляем акцию из спровочника
			                                      	//проверяем, чтобы акции не было в каком-нибудь портфеле
			                                      	foreach(var p in Portfolios)
			                                     	{
			                                     		foreach(var pos in p.Positions)
			                                     		{
			                                     			if( pos.ActiveId == st.Id )
			                                     			{
			                                     				MessageBox.Show("Такая ETF есть в портфеле '" + p.Name + "'");
			                                     				return;
			                                     			}
			                                     		}
			                                     	}
			                                      	_storage.BeginTransaction();			                                      	
			                                      	st.Delete();
			                                      	_storage.Commit();
			                                      	Etfs.Remove(st);
			                                      }, o => o != null);

			RemoveEtfTypeCommand = new RelayCommand( o => 
			                                          {
			                                          	var st = (EtfTypeViewModel) o;
			                                          	//проверить, чтобы такой тип нигде не использовался
			                                          	var et = Etfs.FirstOrDefault(x => x.EtfType == st);
			                                          	if( et != null )
			                                          	{
			                                          		MessageBox.Show("Данные тип используется у ETF \"" + et.FullName + "\"\nУдаление невозможно");
			                                          		return;
			                                          	}
			                                          	EtfTypes.Remove(st);
			                                          	st.Delete();
			                                          }, o => o != null);
			#endregion

			UpdatePositionsCommand = new RelayCommand(o => Positions = null);
			
			ShowOblDictCommand = new RelayCommand(o => new dlgObl() 
			                                      {
													DataContext = new DictOblViewModel(_storage)
												  }.ShowDialog());

			NavHomeCommand 			= new RelayCommand(o => Navigate<HomePage>(this));
			NavCurrencyDictCommand 	= new RelayCommand(o => Navigate<CurrencyPage>(this));
			NavStocksDictCommand	= new RelayCommand(o => Navigate<StockPage>(this));
			NavBondsDictCommand		= new RelayCommand(o => Navigate<BondPage>(this));
			NavEtfDictCommand		= new RelayCommand(o => Navigate<EtfPage>(this));
			NavCommonPortfolioCommand = new RelayCommand(o => Navigate<CommonPortfolioPage>(this));
			NavVDOCommand 			= new RelayCommand(o => Navigate<VDOPage>(this));

			StatByTypeCommand		= new RelayCommand(o => {
			                                      		StatObject = null;
			                                      		StatByType = true;
			                                      		StatByPortfolio = false;
			                                      		StatByAll = false;
			                                      		StatType = StatTypes.type;
			                                      		OnPropertyChanged("Statistica");
			                                      });
			StatByPortfolioCommand	= new RelayCommand(o => {
			                                          	StatObject = null;
			                                      		StatByPortfolio = true;
			                                      		StatByType = false;
			                                      		StatByAll = false;
														StatType = StatTypes.portfolio;
														OnPropertyChanged("Statistica");
			                                      });
			StatByAllCommand	= new RelayCommand(o => {
			                                          	StatObject = null;
			                                      		StatByPortfolio = false;
			                                      		StatByType = false;
			                                      		StatByAll = true;
														StatType = StatTypes.all;
														OnPropertyChanged("Statistica");
			                                      });

			StatClickCommand 		= new RelayCommand(StatClick);

			AddCuponCommand			= new RelayCommand(AddCuponToBond);
			AddAmortCommand			= new RelayCommand(AddAmortToBond);
			RemoveCuponCommand		= new RelayCommand(RemoveCupon, CanRemoveCupon);
			RemoveAmortCommand		= new RelayCommand(RemoveAmort, CanRemoveAmort);

			_statActor.OnUpdateVolume += pos => {
				if( pos.Portfolio == null )
					Debug.WriteLine("OnUpdateVolume" + ", " + pos.FullName);
				else
					Debug.WriteLine("OnUpdateVolume" + ", " + pos.Portfolio.Name + ", " + pos.FullName);
				Statistica = null;
				//UpdatePercents();
			};

			HoverCommand = new RelayCommand(o => {
			                                	var cp = (ChartPoint)o;	
			                                	Debug.WriteLine("hover: " + cp.SeriesView.Title);
			                                	
			                                });//TODO надо чтобы надпись hover-сектора подсвечивалась и выходила на передний план

			ShowCuponsCommand = new RelayCommand(ShowCupons);
			
			Navigate<HomePage>(this);

			//заглушки
			UMDManager.Instance
				.AddStub<CurrencyViewModel>(1, "Рубль", a => {
				                            	a.Symbol = "RUB";
				                            })
				.AddStub<CurrencyViewModel>(2, "Доллар", a => {
				                            	a.Symbol = "USD";
				                            })
				.AddStub<CurrencyViewModel>(3, "Евро", a => {
				                            	a.Symbol = "EUR";
				                            });
			StatType = StatTypes.portfolio;
		}

		public SettingsViewModel Settings{
			get{
				if( _settings == null )
				{
					_settings = UMDQuery.First<SettingsViewModel>();
					if( _settings == null )
					{
						_settings = new SettingsViewModel(_storage);
						_settings.Insert();
					}
				}
				return _settings;
			}
		}
		SettingsViewModel _settings = null;

        public ObservableCollection<PortfolioViewModel> Portfolios
        {
        	get{
        		if( _portfolios == null )
        		{
        			_portfolios = UMDQuery
        				.Select<PortfolioViewModel>()
        				.OrderBy(x => x.PP).ToList().ToOC();
        			foreach(var p in _portfolios)
        				p.Main = this;
        			_portfolios.RePP();
        		}
        		return _portfolios;
        	}
        }
        ObservableCollection<PortfolioViewModel> _portfolios = null;

        public ObservableCollection<CurrencyViewModel> Currs{
        	get{
        		if( _currs == null )
        		{
        			_currs = UMDQuery.Select<CurrencyViewModel>().OrderBy(x => x.Id).ToOC();
        		}
        		return _currs;
        	}
        }
        ObservableCollection<CurrencyViewModel> _currs = null;

        #region навигация по страницам
        Dictionary<Tuple<int, int>, Page> _pages = new Dictionary<Tuple<int, int>, Page>();

        public void Navigate<T>(object context)
        {
        	//генерим ключ
        	var k1 = context.GetHashCode();
        	var k2 = typeof(T).GetHashCode();
        	var key = Tuple.Create<int, int>(k1, k2);
        	if( !_pages.ContainsKey(key) ) //такой страницы еще нет
        	{
    	    	var page = (Page)Activator.CreateInstance(typeof(T), context);
    	    	_pages[key] = page;
    	    	CurrentPage = page;
        	}
        	else
        	{//такая страница уже есть
        		CurrentPage = _pages[key];
        	}
        }

        public Page CurrentPage{
        	set{
        		_currentPage = value;
        		OnPropertyChanged("CurrentPage");
        	}        
        	get{
        		return _currentPage;
        	}        	
        }
        Page _currentPage = null;
        #endregion

		#region Notify
		public event PropertyChangedEventHandler PropertyChanged;
	    protected virtual void OnPropertyChanged(string propertyName)
	    {
	    	if( PropertyChanged != null )
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
		#endregion

		#region тесты
		public Func<ChartPoint, string> PointLabel { get; set; }
		#endregion

		#region акции
		public ObservableCollection<StockViewModel> Stocks{
			get{
				if( _stocks == null )
				{
					_stocks = UMDQuery.Select<StockViewModel>().OrderBy(x => x.Name).ToOC();
					
					foreach(var s in _stocks )
					{
						s.Main = this;
					}
				}
				return _stocks;
			}
		}
		ObservableCollection<StockViewModel> _stocks = null;

		public ObservableCollection<StockTypeViewModel> StockTypes{
			get{
				if( _stockTypes == null )
				{
					_stockTypes = UMDQuery.Select<StockTypeViewModel>().OrderBy(x => x.Name).ToOC();
				}
				return _stockTypes;
			}
		}
		ObservableCollection<StockTypeViewModel> _stockTypes = null;
		
		public ObservableCollection<OtraslViewModel> Otrasls{
			get{
				if( _otrasls == null )
				{
					_otrasls = UMDQuery.Select<OtraslViewModel>().OrderBy(x => x.Name).ToOC();
				}
				return _otrasls;
			}
		}
		ObservableCollection<OtraslViewModel> _otrasls = null;
		#endregion


		#region облигации
		public ObservableCollection<BondViewModel> Bonds{
			get{
				if( _bonds == null )
				{
					_bonds = UMDQuery.Select<BondViewModel>().OrderBy(x => x.Name).ToOC();
					
					foreach(var s in _bonds )
					{
						s.Main = this;
					}
				}
				return _bonds;
			}
		}

		ObservableCollection<BondViewModel> _bonds = null;
		
		public ObservableCollection<BondTypeViewModel> BondTypes{
			get{
				if( _bondTypes == null )
				{
					_bondTypes = UMDQuery.Select<BondTypeViewModel>().OrderBy(x => x.Name).ToOC();
				}
				return _bondTypes;
			}
		}
		ObservableCollection<BondTypeViewModel> _bondTypes = null;
		#endregion


		#region ETF
		public ObservableCollection<EtfViewModel> Etfs{
			get{
				if( _etfs == null )
				{
					_etfs = UMDQuery.Select<EtfViewModel>().OrderBy(x => x.Name).ToOC();
					
					foreach(var s in _etfs )
					{
						s.Main = this;
					}
				}
				return _etfs;
			}
		}
		ObservableCollection<EtfViewModel> _etfs = null;

		public ObservableCollection<EtfTypeViewModel> EtfTypes{
			get{
				if( _etfTypes == null )
				{
					_etfTypes = UMDQuery.Select<EtfTypeViewModel>().OrderBy(x => x.Name).ToOC();
				}
				return _etfTypes;
			}
		}
		ObservableCollection<EtfTypeViewModel> _etfTypes = null;
		#endregion

		#region справочник купонов
	    private void AddCuponToBond(object o)
	    {
	    	_storage.BeginTransaction();
	    	var obl = (BondViewModel) o;
	    	double delta = 0;
	    	decimal size = 0;
	    	if( obl.Cupons.Count >= 2 )
	    	{
	    		var d1 = obl.Cupons[obl.Cupons.Count - 1].DT;
	    		var d2 = obl.Cupons[obl.Cupons.Count - 2].DT;
	    		delta = (d1 - d2).TotalDays;
	    		size = obl.Cupons[obl.Cupons.Count - 1].Size;
	    	}

	    	var cup = new CuponViewModel(_storage)
	    	{
	    		OblId = obl.Id,
	    		Size = size
	    	};
	    	cup.Insert();
	    	if( delta > 0 )
	    		cup.DT = obl.Cupons[obl.Cupons.Count - 1].DT.AddDays(delta);
	    	else
	    		cup.DT = DateTime.Now;
	    	obl.Cupons.Add(cup);
	    	obl.Cupons.RePP();
	    	_storage.Commit();
	    }

	    bool CanRemoveCupon(object o)
	    {
	    	if( o == null )
	    		return false;
	    	var t = (Tuple<BondViewModel, CuponViewModel>)o;
	    	if( t.Item1 == null )
	    		return false;
	    	if( t.Item2 == null )
	    		return false;
	    	return true;
	    }

	    bool CanRemoveAmort(object o)
	    {
	    	if( o == null )
	    		return false;
	    	var t = (Tuple<BondViewModel, AmortViewModel>)o;
	    	if( t.Item1 == null )
	    		return false;
	    	if( t.Item2 == null )
	    		return false;
	    	return true;
	    }

	    private void AddAmortToBond(object o)
	    {
	    	_storage.BeginTransaction();
	    	var obl = (BondViewModel) o;

//	    	var n = new AmortModel()
//	    	{
//	    		OblId = obl.Id,
//	    		Size = 0
//	    	};
//	    	_storage.Insert(n);
	    	var amort = new AmortViewModel(_storage)
	    	{
	    		OblId = obl.Id,
	    		Size = 0
	    	};
	    	amort.Insert();
	    	amort.DT = DateTime.Now;
	    	obl.Amorts.Add(amort);
	    	obl.Amorts.RePP();
	    	//amort.PP = obl.Amorts.Count;
	    	//MessageBox.Show("Добавляем купон в " + obl.FullName);
	    	_storage.Commit();
	    }

	    void RemoveCupon(object o)
	    {
	    	var t = (Tuple<BondViewModel, CuponViewModel>)o;
	    	t.Item1.RemoveCupon(t.Item2);
	    }

	    void RemoveAmort(object o)
	    {
	    	var t = (Tuple<BondViewModel, AmortViewModel>)o;
	    	t.Item1.RemoveAmort(t.Item2);
	    }
	    #endregion

	    #region Общий портфель
	    public ObservableCollection<PositionViewModel> Positions
	    {
	    	get{
//	    		if( _positions == null )
//	    		{
	    			_positions = new ObservableCollection<PositionViewModel>();
	    			foreach(var port in Portfolios)
	    			{
	    				foreach(var pos in port.Positions)
	    				{
	    					if( !_positions.Any(x => x.ActiveType == pos.ActiveType && x.ActiveId == pos.ActiveId) ) //таково актива еще не было
	    					{
	    						var p = new PositionViewModel(_storage)
	    						{
	    							Main = this,
	    							RealTimeUpdate = false,
	    							ParentId = -1,
	    							ActiveId = pos.ActiveId,
	    							ActiveType = pos.ActiveType,
	    							Volume = pos.Volume,
	    							AvgPrice = pos.AvgPrice, //надо вычислять среднюю цену нормально
	    							Positions = new ObservableCollection<PositionViewModel>()
	    						};
	    						p.Positions.Add(pos);
	    						_positions.Add(p);
	    					}
	    					else
	    					{//надо приплюсоваться к существующей позиции
	    						var p = _positions.First(x => x.ActiveType == pos.ActiveType && x.ActiveId == pos.ActiveId);
	    						p.Volume += pos.Volume;
	    						p.Positions.Add(pos);
	    					}
	    				}
	    			}
//	    		}
	    		return _positions;
	    	}
	    	set{
	    		_positions = value;
	    		OnPropertyChanged("Positions");
	    	}
	    }
	    ObservableCollection<PositionViewModel> _positions = null;

	    public decimal TotalCost{
	    	get{
	    		if( _totalCost <= 0 )
	    		{
	    			_totalCost = 0;
	    			foreach(var por in Portfolios)
	    			{
	    				foreach(var pos in por.Positions)
	    					_totalCost += pos.TotalCost;
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

        public void UpdatePercents()
        {
        	TotalCost = -1;
        	var ttt = TotalCost; //чтобы обновилось
        	foreach(var p in Positions)
        	{
        		p.Percent = 0;
        		ttt = p.Percent;
        	}
        }
	    #endregion

	    #region купоны по месяцам
	    List<string> _months = new List<string>{"Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август",
	    	"Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};

	    decimal GetCupons(int aMonthShift)
	    {
	    	var dt = DateTime.Now.AddMonths(aMonthShift);
	    	decimal res = 0;
	    	foreach(var p in Portfolios) //ищем по все портфелям
	    	{
	    		foreach(var pos in p.Positions.Where(x => x.ActiveType == ActiveTypes.bonds) )	//ищем по всем позициям
	    		{
    				var bond = pos.Active as BondViewModel;
    				foreach(var c in bond.Cupons)
    				{
    					if( c.DT.Month == dt.Month && c.DT.Year == dt.Year ) //месяц и год совпадает
    					{
    						if( aMonthShift == 0 )
    						{
    							if( c.DT.Day > dt.Day ) //если месяц текущий, то показываем только те купоны,
    								//которые еще не выплатили
    								res += c.Size*pos.Volume;
    						}
    						else
    							res += c.Size*pos.Volume;
    					}
    				}
	    		}
	    	}
	    	return res;
	    }

	    List<Tuple<PositionViewModel, CuponViewModel, decimal>> GetListofCupons(int aMonthShift)
	    {
	    	var dt = DateTime.Now.AddMonths(aMonthShift);
	    	var res = new List<Tuple<PositionViewModel, CuponViewModel, decimal>>();
	    	foreach(var p in Portfolios) //ищем по все портфелям
	    	{
	    		foreach(var pos in p.Positions.Where(x => x.ActiveType == ActiveTypes.bonds) )	//ищем по всем позициям
	    		{
    				var bond = pos.Active as BondViewModel;
    				foreach(var c in bond.Cupons)
    				{
    					if( c.DT.Month == dt.Month && c.DT.Year == dt.Year ) //месяц и год совпадает
    					{
    						if( aMonthShift == 0 )
    						{
    							if( c.DT.Day > dt.Day ) //если месяц текущий, то показываем только те купоны,
    								//которые еще не выплатили
    								res.Add(Tuple.Create(pos, c, c.Size*pos.Volume));
    								//res += c.Size*pos.Volume;
    						}
    						else
    							//res += c.Size*pos.Volume;
    							res.Add(Tuple.Create(pos, c, c.Size*pos.Volume));
    					}
    				}
	    		}
	    	}
	    	return res;
	    }


	    public string MName1{
	    	get{	    		
	    		return _months[DateTime.Now.Month - 1];
	    	}
	    }
	    public decimal M1{
	    	get{
	    		return GetCupons(0);
	    	}
	    }

	    public string MName2{
	    	get{
	    		switch(DateTime.Now.Month)
	    		{
	    			case 12: return _months[0]; //следующий год
	    			default: return _months[DateTime.Now.Month];
	    		}
	    	}
	    }

	    public decimal M2{
	    	get{
	    		return GetCupons(1);
	    	}
	    }

	    public string MName3{
	    	get{
	    		switch(DateTime.Now.Month)
	    		{
	    			case 11: return _months[0]; //следующий год
	    			case 12: return _months[1]; //следующий год
	    			default: return _months[DateTime.Now.Month + 1];
	    		}
	    	}
	    }

	  	public decimal M3{
	    	get{
	    		return GetCupons(2);
	    	}
	    }

	    public ObservableCollection<FutureCuponViewModel> FutureCupons{
	    	get{
	    		return null;
	    	}
	    }

	    void ShowCupons(object o)
	    {
	    	var i = int.Parse((string)o);
	    	Debug.WriteLine(i);
	    	
	    	var dlg = new Cupons()
	    	{
	    		DataContext = this
	    	};

	    	switch(i)
	    	{
	    		case 0: dlg.Title = "Купоны за " + MName1;break;
	    		case 1: dlg.Title = "Купоны за " + MName2; break;
	    		case 2: dlg.Title = "Купоны за " + MName3; break;
	    	}
	    	dlg.DataContext = GetListofCupons(i).OrderBy(x => x.Item2.DT);

	    	dlg.ShowDialog();
	    }

	    #endregion
	}
}

//TODO на форме изменения портфеля добавить кнопку "Ок", а кнопку "Поменять" (цвет) переименовать в "Изменить цвет"
//TODO !!! когда удаляю позицию, подвтверждать действие

//TODO статистика по отраслям
 