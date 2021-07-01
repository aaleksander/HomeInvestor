using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// будущая выплата купона
	/// </summary>
	public class FutureCuponViewModel: ViewModelBase
	{
		public FutureCuponViewModel()
		{
		}
		
		#region поля
		public BondViewModel Bond{
			set;get;
		}
		
		
		#endregion
	}
}
