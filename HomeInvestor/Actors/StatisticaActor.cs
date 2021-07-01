using System;
using HomeInvestor.ViewModels;

namespace HomeInvestor.Actors
{
	/// <summary>
	/// Description of StatisticaActor.
	/// </summary>
	public class StatisticaActor: ActorBase
	{
		public StatisticaActor(): base("stat", null)
		{
			OnUpdateVolume = (o) => {};
		}

		public delegate void OnUpdateVolumeContainer(PositionViewModel pos);
		public event OnUpdateVolumeContainer OnUpdateVolume;

		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgUpdateVolume )
			{
				OnUpdateVolume((aMsg as MsgUpdateVolume).Positon);
			}

			return null;
		}

	}
}
