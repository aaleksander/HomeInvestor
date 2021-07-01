using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of HistoryViewModel.
	/// </summary>
	[Alias("History")]
	[Properties(
		"dt", "data",
		"int", "volume",
		"str", "operation",
		"dec", "price"
	)]
	/// <summary>
	/// история позиции
	/// </summary>
	public class HistoryViewModel: UMDViewModelBase
	{
		//в качестве родителя выступает Parent_id
		
		public HistoryViewModel(IStorage st): base(st)
		{
		}

		public HistoryViewModel(IStorage st, UMDObjectModel m): base(st, m)
		{
		}

		#region поля
		public DateTime DT
		{
			set{
				SetProp(0, value);
				OnPropertyChanged("DT");
			}
			get{
				return GetDT(0);
			}
		}

		public int Volume
		{
			set{
				SetProp(1, value);
				OnPropertyChanged("Volume");
			}
			get{
				return GetInt(1);
			}
		}

		public string Operation
		{
			set{
				SetProp(2, value);
				OnPropertyChanged("Operation");
			}
			get{
				return GetStr(2);
			}
		}
		
		public decimal Price
		{
			set{
				SetProp(3, value);
				OnPropertyChanged("Price");
			}
			get{
				return GetDecimal(3);
			}
		}
		#endregion

		public PositionViewModel Position{
			get{
				return UMDQuery.ById<PositionViewModel>(ParentId);
			}
		}
	}
}
