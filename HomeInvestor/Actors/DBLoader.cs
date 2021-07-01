using System;
using UMD;

namespace HomeInvestor.Actors
{
	public class MsgLoadDB
	{
		
	}

	/// <summary>
	/// Description of DBLoader.
	/// </summary>
	public class DBLoader: ActorBase
	{
		public DBLoader(): base(names.loader, null)
		{
			OnLoaded = () => {};
		}

		public Storage Storage;

		protected override object OnReceive(object aMsg)
		{
			if( aMsg is MsgLoadDB )
			{
				DebugPrint("начинаю загружать");
				Storage = new Storage();
				UMDManager.Storage = Storage;
				UMDManager.RegCreators();
				Storage.FileName = "data.sqlite";
				UMDManager.Instance.FillCache();

				if( OnLoaded != null )
					OnLoaded();
			}
			return null;
		}

		public delegate void OnLoadedContainer();
		public event OnLoadedContainer OnLoaded;
	}
}
