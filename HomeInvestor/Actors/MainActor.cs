using System;
using HomeInvestor.ViewModels;
using UMD;

namespace HomeInvestor.Actors
{
	public class MsgRemovePortfolio	
	{
		public MsgRemovePortfolio(PortfolioViewModel p)
		{
			Portfolio = p;
		}
		public PortfolioViewModel Portfolio{private set;get;}
	}

	/// <summary>
	/// Description of MainActor.
	/// </summary>
	public class MainActor: ActorBase
	{
		public MainActor(MainViewModel parent): base(names.mainVM, parent)
		{
			_parent = parent;
			OnRemovePortfolio = o => {};
			OnFocus = (a, b) => {};
		}

		MainViewModel _parent;
		public delegate void OnRemovePortfolioContainer(PortfolioViewModel p);
		public event OnRemovePortfolioContainer OnRemovePortfolio;
		
		public delegate void OnFocusContainer(PortfolioViewModel port, PositionViewModel pos);
		public event OnFocusContainer OnFocus;

		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgFocus)
			{
				var m = aMsg as MsgFocus;
				var port = m.Portfolio;
				var pos  = m.Position;
				if( OnFocus != null )
				{
					App.Current.Dispatcher.Invoke(()=>{
						OnFocus(port, pos);
					                              });
				}
			}
			
			if( aMsg is MsgRemovePortfolio ) //удаляется портфель
			{
				var p = (aMsg as MsgRemovePortfolio).Portfolio;
				//удаляем позиции в потфеле
//				foreach(var pp in p.Positions)
//				{
//					pp.Delete();
//				}
				p.Delete();
				App.Current.Dispatcher.Invoke(()=>{
					_parent.Portfolios.Remove(p);
				                              });
			}

			if( aMsg is MsgNewPortfolio )	//добаляется потфель
			{
				var p = (aMsg as MsgNewPortfolio).Portfolio;
				p.Insert();
				App.Current.Dispatcher.Invoke(()=>{
												_parent.Portfolios.Add(p);
												_parent.Portfolios.RePP();
				                              });
			}

			return null;
		}
	}
}
