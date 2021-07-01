using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// настройки
	/// </summary>
	[Alias("settings")]
	[Properties(
		"dec", "dollar",
		"dec", "eur"
	)]
	public class SettingsViewModel: UMDViewModelBase
	{
		public SettingsViewModel(IStorage st): base(st)
		{
		}

		public SettingsViewModel(IStorage st, UMDObjectModel m): base(st, m)
		{
		}

		#region поля
		public decimal DollarPrice{
			set{
				SetProp(0, value);
				OnPropertyChanged("DollarPrice");
				//TODO послать сообщение, что даллар поменялся
			}
			get{
				return GetDecimal(0);
			}
		}

		public decimal EuroPrice{
			set{
				SetProp(1, value);
				OnPropertyChanged("EuroPrice");
				//TODO послать сообщение, что евро поменялось
			}
			get{
				return GetDecimal(1);
			}
		}
		#endregion
	}
}

//TODO сделать настройку "Разворачивать на полный экран"
