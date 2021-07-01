using System;
using System.Collections.ObjectModel;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Работа с условиями
	/// </summary>
	public partial class MainViewModel
	{		
		void UpdateConditions(object o)
		{
			Conditions = null;
		}

		public ObservableCollection<ConditionViewModel> Conditions{
			get{
				if( _conditions == null )
				{
					_conditions = new ObservableCollection<ConditionViewModel>();
					
					foreach(var st in Stocks)
					{
						foreach(var c in st.Conds)
						{
							_conditions.Add(new ConditionViewModel(c, st, this));
						}
					}
					
					foreach(var st in Bonds)
					{
						foreach(var c in st.Conds)
						{
							_conditions.Add(new ConditionViewModel(c, st, this));
						}
					}
				}
				return _conditions;
			}
			set{
				_conditions = value;
				OnPropertyChanged("Conditions");
			}
		}
		ObservableCollection<ConditionViewModel> _conditions;
	}
}
