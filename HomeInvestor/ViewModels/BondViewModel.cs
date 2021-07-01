using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using HomeInvestor.Actors;
using HomeInvestor.CondParser;
using HomeInvestor.Dlg;
using UMD;

namespace HomeInvestor.ViewModels
{
	//TODO если облигация погашена, то отмечать сереньким (но не удалять). И сделать галочку (показывать только те, что в обороте)
	/// <summary>
	/// облигация
	/// </summary>
	[Alias("obl")]
	[Properties(
		"int", "type_id",
		"str", "short_name",
		"str", "full_name",
		"str", "isin",
		"real", "nominal",
		"str", "pogash_dt",
		"int", "next_obl_id",
		"float", "price",
		"float", "dohod",
		"str", "raschet",
		"int", "bond_type",
		"int", "currNominalId",
		"int", "currCuponId",
		"str", "notes",
		"str", "vypusk"
	)]
	public class BondViewModel: UMDViewModelBase
	{
		public BondViewModel(IStorage st):base(st)
		{
			Init();
		}	

		public BondViewModel(IStorage st, UMDObjectModel m):base(st, m)
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
		public int TypeId
		{
			set{
				if( value != GetInt(0) )
				{
					SetProp(0, value);
					UpdateIfNeed();
					OnPropertyChanged("TypeId");
					OnPropertyChanged("Type");
				}
			}
			get{
				return GetInt(0);
			}
		}

		public string ShortName
		{
			set{
				if( value != GetStr(1) )
				{
					SetProp(1, value);
					UpdateIfNeed();
					OnPropertyChanged("ShortName");
				}
			}
			get{
				return GetStr(1);
			}
		}

		public string FullName
		{
			set{
				if( value != GetStr(2) )
				{
					SetProp(2, value);
					UpdateIfNeed();
					OnPropertyChanged("FullName");
				}
			}
			get{
				return GetStr(2);
			}
		}

		public string ISIN
		{
			set{
				if( value != GetStr(3) )
				{
					SetProp(3, value);
					UpdateIfNeed();
					OnPropertyChanged("ISIN");
				}
			}
			get{
				return GetStr(3);
			}
		}
		
		public Decimal Nominal
		{
			set{
				if( value != GetDecimal(4) )
				{
					SetProp(4, value);
					UpdateIfNeed();
					OnPropertyChanged("Nominal");
				}
			}
			get{
				return GetDecimal(4);
			}
		}

		public DateTime PogashDT
		{
			set{
				SetProp(5, value);
				UpdateIfNeed();
				OnPropertyChanged("PogashDT");
			}
			get{
				try
				{
					return GetDT(5);
				}catch(FormatException)
				{
					return DateTime.Now;
				}
			}
		}
		
		public int NextOblId
		{
			set{
				if( value == GetInt(6) )
				{
					MessageBox.Show("Нельзя заменить на ту же самую облигацию");
					return;
				}

				if( value != GetInt(6) )
				{
					SetProp(6, value);
					UpdateIfNeed();
					OnPropertyChanged("NextOblId");
					OnPropertyChanged("NextObl");
				}				
			}
			get{
				return GetInt(6);
			}
		}

		/// <summary>
		/// текущая рыночная цена
		/// </summary>
		public decimal Price{
			set{
				SetProp(7, value);
				OnPropertyChanged("Price");
				OnPropertyChanged("DohToPogash");
				ActorSystem.Instance.Tell<PositionActor>(new MsgChangePrice(this));
			}
			get{
				return GetDecimal(7);
			}
		}

		/// <summary>
		/// годовая доходность
		/// </summary>
		public decimal Dohod{
			set{
				SetProp(8, value);
				OnPropertyChanged("Dohod");
			}
			get{
				return GetDecimal(8);
			}
		}
		
		public string Raschet{
			set{
				SetProp(9, value);
				OnPropertyChanged("Raschet");
			}
			get{
				return GetStr(9);
			}
		}

		public int BondTypeId{
			set{
				SetProp(10, value);
				OnPropertyChanged("BondTypeId");
				OnPropertyChanged("BondType");
				OnPropertyChanged("TypeName");
			}
			get{
				return GetInt(10);
			}
		}

		public int CurrNominalId{
			set{
				SetProp(11, value);
				OnPropertyChanged("CurrNominalId");
			}
			get{
				return GetInt(11);
			}
		}

		public int CurrCuponId{
			set{
				SetProp(12, value);
				OnPropertyChanged("CurrCuponId");
			}
			get{
				return GetInt(12);
			}
		}

		public string Notes{
			set{
				SetProp(13, value);
				OnPropertyChanged("Notes");
			}
			get{
				return GetStr(13);
			}
		}

		/// <summary>
		/// дата выпуска (для расчета возраста)
		/// </summary>
		public DateTime VypuskDT
		{
			set{
				SetProp(14, value);
				UpdateIfNeed();
				OnPropertyChanged("VypuskDT");
			}
			get{
				try
				{
					return GetDT(14);
				}catch(FormatException)
				{
					//попытаемся родить дату из первых купонов
					if( Cupons.Count >= 2 )
					{
						var dt = Cupons[1].DT - Cupons[0].DT;
						var dt2 = Cupons[0].DT.AddDays(-dt.TotalDays);						
						return dt2;
					}
					return DateTime.Now;
				}
			}
		}
		#endregion
		
		public int Age{
			get{
				return (int)(DateTime.Now - VypuskDT).TotalDays;
			}
		}

		public BondTypeViewModel BondType{
			get{
				return UMDQuery.First<BondTypeViewModel>(x => x.Id == BondTypeId);
			}
		}

		public ObservableCollection<CuponViewModel> Cupons
		{
			get{
				if( _cupons == null )
				{
					_cupons = UMDQuery.Select<CuponViewModel>().Where(x => x.OblId == this.Id).OrderBy(x => x.DT).ToOC();
				}

				if( !_cupons.Any(x => x.First) )
				{
					//выделим ближайший купон
					var f = false;
					foreach(var c in _cupons)
					{
						if( c.DT > DateTime.Now && !f )
						{
							f = true;
							c.First = true;
							break;
						}
					}
				}
				return _cupons;
			}
		}
		private ObservableCollection<CuponViewModel> _cupons = null;

		public ObservableCollection<AmortViewModel> Amorts
		{
			get{
				if( _amorts == null )
				{
					//var pp = 1;
					_amorts = UMDQuery.Select<AmortViewModel>().Where(x => x.OblId == this.Id).OrderBy(x => x.DT).ToOC();
				}
				
				if( !_amorts.Any(x => x.First) )
				{
					//выделим ближайшую амортизацию
					var f = false;
					foreach(var c in _amorts)
					{
						if( c.DT > DateTime.Now && !f )
						{
							f = true;
							c.First = true;
							break;
						}
					}
				}
				return _amorts;
			}
		}
		private ObservableCollection<AmortViewModel> _amorts = null;

		/// <summary>
		/// сколько дней осталось до погашения
		/// </summary>
		public int DaysLeftPogash{
			get{
				return (int)(PogashDT - DateTime.Now.Date).TotalDays;
			}
		}

		/// <summary>
		/// макcимально возможный купонный доход
		/// </summary>
		public decimal MaxNCD
		{
			get{
				//ближайший купон
				var cup = Cupons.Where(x => x.DT >= DateTime.Now).OrderBy(x => x.DT).FirstOrDefault();			

				if( cup != null )
					return cup.Size;
				return 0;
			}
		}

		public int DaysLeft
		{
			get{
				var next = Cupons.Where(x => x.DT >= DateTime.Now).OrderBy(x => x.DT).FirstOrDefault();
				if( next == null )
					return 0;
				
				return (int)(next.DT - DateTime.Now.Date).TotalDays;
			}
		}

		public decimal NCD
		{
			get{
				if( _ncd < 0 )
				{
					var next = Cupons.Where(x => x.DT >= DateTime.Now).OrderBy(x => x.DT).FirstOrDefault();
					var last = Cupons.Where(x => x.DT < DateTime.Now).OrderBy(x => x.DT).LastOrDefault();
					if( last == null || next == null )
						return 0;
					var daysAll = (int)(next.DT - last.DT).TotalDays;
					var days = (int)(DateTime.Now - last.DT).TotalDays;
					var inDay = next.Size/daysAll; //купонов в день
					_ncd = inDay*(decimal)days;
				}

				return _ncd;
			}
		}
		decimal _ncd = -1;

		/// <summary>
		/// номинал на текущий момент
		/// </summary>
		public decimal NominalNow
		{
			get
			{
				return NominalForDT(DateTime.Now);
			}
		}

		public decimal NominalForDT(DateTime dt)
		{
			if( PogashDT < dt ) //погасилась, ничего не стОит
				return 0M;
			if( Amorts.Count == 0 ) //амортизаций нет, возвращаем номинал
				return Nominal;

			//начинаем рассчитывать
			var res = Nominal;
			foreach(var a in Amorts)
			{
				if( a.DT <= dt )
					res -= a.Size;
			}

			return res;
		}

		public void RemoveCupon(CuponViewModel cup)
		{
			cup.Delete();
			Cupons.Remove(cup);
		}

		public void RemoveAmort(AmortViewModel amort)
		{
			amort.Delete();
			Amorts.Remove(amort);
		}

		//Пересчитывает доход облигации
		public void UpdateDohod()
		{
			var dt = DateTime.Now.Date;
			decimal count = 1; //количество облигаций
			decimal rashod = (NominalForDT(dt)*Price/100 + NCD)*count; //расход
			decimal freeMoney = 0;	//свободные деньги, которые надо реинвестировать
			int days = 0;

			CuponViewModel cupon = null;
			AmortViewModel amort = null;
			var sb = new StringBuilder();
			var tmp = "";
			sb.AppendLine(string.Format("Покупаем одну облигацию, потратили с учетом НКД {0:F2}", rashod));			
			while( dt <= PogashDT )
			{
				cupon = Cupons.FirstOrDefault(x => x.DT == dt);
				if( cupon != null )
				{
					sb.AppendLine("");
					tmp = string.Format("{0}. Купон! {1}, итого {2:F2}", dt.ToStr(), cupon.Size, count*cupon.Size);
					Debug.WriteLine(tmp);
					sb.AppendLine(tmp);
					freeMoney += count*cupon.Size;
				}

				amort = Amorts.FirstOrDefault(x => x.DT == dt);
				if( amort != null )
				{
					tmp = string.Format("{0}. Амортизация! {1}", dt.ToStr(), amort.Size);
					Debug.WriteLine(tmp);
					sb.AppendLine(tmp);
					freeMoney += count*amort.Size;
				}

				dt = dt.AddDays(1);
				days++;
			}

			days--;

			sb.AppendLine(string.Format("Свободных денег: {0:F2}", freeMoney));
			//возращаем деньги по номиналу
			//если облига без амотризации, то надо в сумму вернуть номинал

			if( rashod > 0 )
			{
				Debug.WriteLine("Итого до погашения дней:\t{0}", days);
				sb.AppendLine(string.Format("Итого до погашения дней:\t{0}", days));

				Debug.WriteLine("Итого расход:\t{0}", rashod);
				sb.AppendLine(string.Format("Итого расход:\t{0:F2}", rashod));

				var prc = Math.Floor((freeMoney/rashod - 1)*10000)/100;
				var doh = (freeMoney/rashod - 1)/days*365*100;

				Debug.WriteLine("{1}. Доходность:\t{0:F2}",  doh, ShortName);
				sb.AppendLine(string.Format("{1}. Доходность:\t{0:F2}",  doh, ShortName));

				Debug.WriteLine("");
				Dohod = doh;
			}
			else
				Dohod = 0;
			Raschet = sb.ToString();
		}

		public OblType_Stub Type
		{
			get{
				return UMDQuery.ById<OblType_Stub>(TypeId);
			}
		}

		public ObservableCollection<OblType_Stub> OblTypes{
			get
			{
				if( _oblTypes == null )
				{
					_oblTypes = UMDQuery.Select<OblType_Stub>().ToOC();
				}
				return _oblTypes;
			}
		}
		private static ObservableCollection<OblType_Stub> _oblTypes = null;

		public List<HomeInvestor.CondParser.Condition> Conds{
			get{
			
				var res = new List<HomeInvestor.CondParser.Condition>();
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
		
		/// <summary>
		/// доходность к погашению
		/// </summary>
		public decimal DohToPogash{
			get{
				//ближайший купон
				var cup = Cupons.FirstOrDefault(x => x.First);
				if( cup == null )
					return -1;
				if( Cupons.Count < 2 )
					return -1;

				//определяем сколько раз в год платится
				var days = (Cupons[1].DT - Cupons[0].DT).TotalDays;
				var m = (decimal)Math.Round(365/days);
				Debug.WriteLine(days + ", " + m);

				var price = (Price*NominalNow/100);
				Debug.WriteLine("price = " + price);
				var currDoh = m*cup.Size/price*100.0M;
				Debug.WriteLine("доходность = " + currDoh);
				return currDoh + (NominalNow - price)/price*365/DaysLeftPogash*100.0M;
			}
		}
	}
}

//TODO !!! в таблицах, там где цена, показывать валюту (потом, когда будут еврооблигации)
