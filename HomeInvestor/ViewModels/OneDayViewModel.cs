using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	public enum OD_What {none, 
		buy,	//позиция увеличилась 
		sell, 	//позиция уменьшилась
		cupon, 	//пришли купоны
		divs	//пришли дивиденты
	}

	/// <summary>
	/// один день из жизни позиции
	/// </summary>
	public class OneDayViewModel: ViewModelBase
	{
		public OneDayViewModel()
		{
		}

		/// <summary>
		/// что произошло в этот день
		/// </summary>
		public OD_What What{set;get;}

		/// <summary>
		/// текущий баланс прибыли
		/// </summary>
		public decimal Balance{set;get;}
	}
}
