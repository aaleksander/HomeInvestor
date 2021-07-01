
using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of EtfTypeViewModel.
	/// </summary>
	[Alias("etf_type")]
	[Properties()]
	public class EtfTypeViewModel: UMDViewModelBase
	{
		public EtfTypeViewModel(IStorage st): base(st)
		{
		}
		
		public EtfTypeViewModel(IStorage st, UMDObjectModel obj): base(st, obj)
		{
		}
		
		public override string ToString()
		{
			return "Е, " + Name;
		}
	}
}
