/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 14.11.2019
 * Время: 11:31
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using HomeInvestor.Actors;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of EtfViewModel.
	/// </summary>
	[Alias("etf")]
	[Properties(
		"int", "currency",
		"str", "fullname",
		"str", "ticker",
		"int", "currPriceId",
		"dec", "price",
		"int", "typeid"
	)]
	public class EtfViewModel:UMDViewModelBase
	{
		public EtfViewModel(IStorage st): base(st)
		{
		}
		
		public EtfViewModel(IStorage st, UMDObjectModel o): base(st, o)
		{
		}
		
		public MainViewModel Main{set;get;}
		
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

		public decimal Price{
			set{
				SetProp(4, value);
				OnPropertyChanged("Price");
				ActorSystem.Instance.Tell<PositionActor>(new MsgChangePrice(this));
			}
			get{
				return GetDecimal(4);
			}
		}

		public int EtfTypeId{
			set{
				SetProp(5, value);
				OnPropertyChanged("EtfTypeId");
				OnPropertyChanged("EtfType");
				OnPropertyChanged("TypeName");
			}
			get{
				return GetInt(5);
			}
		}
		#endregion

		public EtfTypeViewModel EtfType{
			get{
				return UMDQuery.First<EtfTypeViewModel>(x => x.Id == EtfTypeId);
			}
		}
	}
}
