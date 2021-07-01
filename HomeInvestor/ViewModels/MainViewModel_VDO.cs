using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// кусок MainViewModel, посвященный ВДО
	/// </summary>
	public partial class MainViewModel
	{
		public ObservableCollection<PositionViewModel> VDOPositions
		{
			get{
				return new ObservableCollection<PositionViewModel>(Positions.Where(x => x.ActiveType == ActiveTypes.bonds 
				                                                                      && (x.Active as BondViewModel).BondType.Name == "ВДО"));
			}
		}		
	}
}
