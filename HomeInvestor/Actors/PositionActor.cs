using System;

namespace HomeInvestor.Actors
{
	/// <summary>
	/// Description of PositionActor.
	/// </summary>
	public class PositionActor: ActorBase
	{
		public PositionActor(string name, object active): base(name, active)
		{
			OnChangePrice = o => {};
		}

		public event OnAction OnChangePrice;

		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgChangePrice )
			{
				var m = (aMsg as MsgChangePrice);
				if( m.Witnessed.GetHashCode() == WitnessedHashCode )
					OnChangePrice((aMsg as MsgChangePrice).Witnessed);
			}
			return null;
		}
	}
}
