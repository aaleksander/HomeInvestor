using System;
using System.Collections.Generic;
using HomeInvestor.Actors;
using HomeInvestor.CondParser;
using HomeInvestor.Dlg;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of StockViewModel.
	/// </summary>
	[Alias("stock")]
	[Properties(
		"int", "currency",
		"str", "fullname",
		"str", "ticker",
		"int", "currPriceId",
		"int", "currDivId",
		"dec", "price",
		"int", "typeid",
		"str", "notes",
		"int", "otraslId"
	)]
	public class StockViewModel: UMDViewModelBase
	{
		public StockViewModel(IStorage st): base(st)
		{
			Init();
		}

		public StockViewModel(IStorage st, UMDObjectModel o): base(st, o)
		{
			Init();
		}

		public RelayCommand NotesCommand{private set;get;}
		public MainViewModel Main{set;get;}

		void Init()
		{
			NotesCommand = new RelayCommand(o => 
			                                {
			                                	var dlg = new dlgNotes();
			                                	dlg.text.Text = Notes;
			                                	dlg.Title = "Заметки по " + FullName;
			                                	var res = dlg.ShowDialog();
			                                	if( res.HasValue && res.Value )
			                                	{
			                                		Notes = dlg.text.Text;
			                                	}
			                                });
		}

		#region поля
		public int CurrId{
			set{
				SetProp(0, value);
				OnPropertyChanged("CurrId");
				OnPropertyChanged("Currrency");
			}
			get{
				return GetInt(0);
			}
		}

		public string FullName{
			set{
				SetProp(1, value);
				OnPropertyChanged("FullName");
			}
			get{
				return GetStr(1);
			}
		}

		public string Ticker{
			set{
				SetProp(2, value);
				OnPropertyChanged("Ticker");
			}
			get{
				return GetStr(2);
			}
		}

		public int CurrPriceId{
			set{
				SetProp(3, value);
				OnPropertyChanged("CurrPriceId");
				OnPropertyChanged("CurrPrice");
			}
			get{
				return GetInt(3);
			}
		}

		public int CurrDivId{
			set{
				SetProp(4, value);
				OnPropertyChanged("CurrDivId");
				OnPropertyChanged("CurrDiv");
			}
			get{
				return GetInt(4);
			}
		}

		public decimal Price{
			set{
				SetProp(5, value);
				OnPropertyChanged("Price");
				ActorSystem.Instance.Tell<PositionActor>(new MsgChangePrice(this));
			}
			get{
				return GetDecimal(5);
			}
		}

		public int StockTypeId{
			set{
				SetProp(6, value);
				OnPropertyChanged("StockTypeId");
				OnPropertyChanged("StockType");
				OnPropertyChanged("TypeName");
			}
			get{
				return GetInt(6);
			}
		}
		
		public string Notes{
			set{
				SetProp(7, value);
				OnPropertyChanged("Notes");
			}
			get{
				return GetStr(7);
			}
		}
		
		public int OtraslId{
			set{
				SetProp(8, value);
				OnPropertyChanged("OtraslId");
				OnPropertyChanged("Otrasl");
			}
			get{
				return GetInt(8);
			}
		}
		#endregion

		public StockTypeViewModel StockType{
			get{
				return UMDQuery.First<StockTypeViewModel>(x => x.Id == StockTypeId);
			}
		}

		public OtraslViewModel Otrasl{
			get{
				return UMDQuery.First<OtraslViewModel>(x => x.Id == OtraslId);
			}
		}

		public List<Condition> Conds{
			get{
				var res = new List<Condition>();
				if( Notes.Length == 0 )
					return res;
				
				var ss = Parser.GetConds(Notes);
				foreach(var s in ss)
				{
					var nc = Parser.Parse(s);
					if( nc != null )
						res.Add(nc);
				}
				return res;
			}
		}
	}
}
