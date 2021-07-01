using System;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Actors
{
	/// <summary>
	/// создать новый портфель
	/// </summary>
	public class MsgNewPortfolio
	{
		public MsgNewPortfolio(PortfolioViewModel p)
		{
			Portfolio = p;
		}
		public PortfolioViewModel Portfolio{private set;get;}
	}

	/// <summary>
	/// актер от MainViewModel
	/// </summary>
	public class MainWindowActor: ActorBase
	{
		public MainWindowActor():base(names.mainWindow, null)
		{
			OnAddPortfolio = (p) => {};
			OnRemovePortfolio = (p) => {};
		}

		public delegate void OnAddPortfolioContainer(PortfolioViewModel p);
		public event OnAddPortfolioContainer OnAddPortfolio;

		public event OnAddPortfolioContainer OnRemovePortfolio;

		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgNewPortfolio && OnAddPortfolio != null )
			{
				OnAddPortfolio((aMsg as MsgNewPortfolio).Portfolio);
			}

			if( aMsg is MsgRemovePortfolio && OnRemovePortfolio != null )
			{
				OnRemovePortfolio((aMsg as MsgRemovePortfolio).Portfolio);
			}
			return null;
		}
	}
}
