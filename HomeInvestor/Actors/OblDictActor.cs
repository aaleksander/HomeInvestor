using System;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Actors
{
	public class MsgAddObl{
		public MsgAddObl(BondViewModel obl)
		{
			Obl = obl;
		}
		public BondViewModel Obl{private set;get;}
	}
	
	/// <summary>
	/// Description of OblDictActor.
	/// </summary>
	public class OblDictActor: ActorBase
	{
		public OblDictActor(DictOblViewModel dict): base(names.oblDict, null)
		{
			_dict = dict;
		}
		
		DictOblViewModel _dict;
		
		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgAddObl )
			{
				var m = aMsg as MsgAddObl;
				var o = m.Obl;
				DebugPrint("дошло");
				o.Insert();
				DebugPrint("выполнил");
			}
			
			return null;
		}
	}
}
