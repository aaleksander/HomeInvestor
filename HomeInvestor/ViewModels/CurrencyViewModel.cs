using System;
using System.Collections.ObjectModel;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Объект - валюта. По сути просто заглушка
	/// </summary>
	/// 
	public class CurrencyViewModel: StubBase
	{
        public CurrencyViewModel(IStorage aSt) : base(aSt) { }
        public CurrencyViewModel(IStorage aSt, UMDObjectModel aModel) : base(aSt, aModel) { }

        public string Symbol{set;get;}
	}
}
