using System;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Actors
{
	/// <summary>
	/// Description of PortfolioActor.
	/// </summary>
	public class PortfolioActor: ActorBase
	{
		public PortfolioActor(string name, PortfolioViewModel portfolio): base(name, portfolio)
		{
			_portfolio = portfolio;
		}
		
		PortfolioViewModel _portfolio;

		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgUpdateVolume ) //поменялась цена или объем
			{
				foreach(var p in _portfolio.Positions)
				{
					//p.TotalCost = 0;
				}
			}
//			if( aMsg is MsgUpdatePercent )
//			{
//				var p = (aMsg as MsgUpdatePercent).Portfolio;
//				if( p == _portfolio )
//				{
//					_portfolio.FutureCuponsSumma = 0;//чтобы обновилось
//				}
//			}
//			
//			if( aMsg is MsgUpdatePlanPercent )
//			{
//				var p = (aMsg as MsgUpdatePlanPercent).Portfolio;
//				if( p == _portfolio )
//				{
//					_portfolio.PlanFutureCuponsSumma = 0;//чтобы обновилось
//					_portfolio.PlanTotalCostByNominal = 0;
//				}
//			}
			return null;
		}
	}
}
