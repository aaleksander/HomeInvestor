using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Амортизация купона
	/// </summary>
	[Alias("amort")]
	[Properties(
		"int", "obl_id",
		"str", "dt",		
		"int", "size"
	)]
	public class AmortViewModel: UMDViewModelBase
	{
		public AmortViewModel(IStorage st): base(st)
		{
		}

		public AmortViewModel(IStorage st, UMDObjectModel m): base(st, m)
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
				var res = GetDT(1);
				return res;
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
		
		public bool First{set;get;}
	}
}
