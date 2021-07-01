using System;

namespace HomeInvestor.Actors
{
	
	public class MsgProgress
	{
		public MsgProgress(double current)
		{
			Current = current;
		}

		public double Current{private set;get;}
	}

	/// <summary>
	/// Description of DBProgress.
	/// </summary>
	public class DBProgress: ActorBase
	{
		public DBProgress(): base(names.progress, null)
		{
			OnProgress = c => {};
		}

		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgProgress )
			{
				var msg = aMsg as MsgProgress;
				
				OnProgress(msg.Current);
			}
			return null;
		}

		public delegate void OnProgressContainer(double curr);
		public event OnProgressContainer OnProgress;	
	}
}
