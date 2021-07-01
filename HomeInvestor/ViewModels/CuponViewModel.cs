using System;
using System.Collections.ObjectModel;
using System.Linq;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Купон по облигации
	/// </summary>
	[Alias("cupon")]
	[Properties(
		"int", "obl_id",
		"str", "dt",
		"real","size"
	)]
	public class CuponViewModel: UMDViewModelBase
	{
		public CuponViewModel(IStorage st): base(st)
		{
		}

		public CuponViewModel(IStorage st, UMDObjectModel m): base(st, m)
		{
		}

		#region поля
		public int OblId
		{
			set
			{
				if( GetInt(0) == value )
					return;
				SetProp(0, value);
				UpdateIfNeed();
				OnPropertyChanged("OblId");
				OnPropertyChanged("Obl");
			}
			get
			{
				return GetInt(0);
			}
		}

		public DateTime DT
		{
			set{
				SetProp(1, value);
				UpdateIfNeed();
				OnPropertyChanged("DT");
			}
			get{
				return GetDT(1);
			}
		}

		public decimal Size
		{
			set{
				if( value != (decimal)GetReal(2) )
				{
					SetProp(2, (double)value);
					UpdateIfNeed();
					OnPropertyChanged("Size");
				}
			}
			get{
				return (decimal)GetReal(2);
			}
		}
		#endregion

		public BondViewModel Obl{
			get{				
				return UMDQuery.First<BondViewModel>(x => x.Id == OblId);
			}
		}

		public bool First{set;get;}
	}
}
