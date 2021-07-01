using System;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Actors
{
	/// <summary>
	/// базовый класс для всех сообщений с содержанием наблюдаемого объекта
	/// </summary>
	public class MsgWitnessedBase
	{
		public MsgWitnessedBase(object o)
		{
			Witnessed = o;
		}
		
		public object Witnessed{private set;get;}
	}

	/// <summary>
	/// У актива поменялась цена
	/// </summary>
	public class MsgChangePrice: MsgWitnessedBase
	{
		public MsgChangePrice(BondViewModel b): base(b)
		{
		}

		public MsgChangePrice(StockViewModel s): base(s)
		{
		}

		public MsgChangePrice(EtfViewModel s): base(s)
		{
		}
	}
	
	public class MsgFocus
	{
		public MsgFocus(PortfolioViewModel port, PositionViewModel pos)
		{
			Portfolio = port;
			Position = pos;
		}

		public PortfolioViewModel Portfolio{private set;get;}
		public PositionViewModel Position{private set;get;}
	}
	
}
