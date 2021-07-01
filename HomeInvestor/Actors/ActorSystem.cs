using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UMD;



namespace HomeInvestor.Actors
{
	/// <summary>
	/// хранит всех акторов, следит за уникальностью имен. Синглетон.
	/// </summary>
	public class ActorSystem: IDisposable
	{
		private ActorSystem()
		{
			_actors = new Dictionary<string, ActorBase>();
		}

		public static ActorSystem Instance{
			get{
				if( _inst == null )
					_inst = new ActorSystem();
				return _inst;
			}
		}
		private static ActorSystem _inst = null;

		private Dictionary<string, ActorBase> _actors;

		public void AddActor(ActorBase a)
		{
			if( _actors.ContainsKey(a.Name) )
				throw new Exception("Актор с таким именем уже существует (" + a.Name + ")");
			_actors[a.Name] = a;
		}

		public void RemoveActor(ActorBase a)
		{
			if( _actors.ContainsKey(a.Name) == false )
			{
				return; 
			//	throw new Exception("Актор с таким именем не существует (" + a.Name + ")");
			}
			_actors.Remove(a.Name);
			a.Dispose();
		}

		public void RemoveActor(string aName)
		{
			if( _actors.ContainsKey(aName) == false )
				return;
				//throw new Exception("Актор с таким именем не существует (" + aName + ")");
			var aa = _actors[aName];
			if( aa.TaskCount > 0 ) 
				Debug.WriteLine("У актора " + aa.Name + " остались задачи. Ждем");
			while(aa.TaskCount > 0 )
				Thread.Sleep(100);
			_actors.Remove(aName);
			aa.Dispose();
		}

		/// <summary>
		/// передать сообщение всем акторам определеного типа
		/// </summary>
		/// <param name="msg"></param>
		public void Tell<T>(object msg) where T: ActorBase  
		{
			//var flag = false;
			foreach (var a in _actors.Values.OfType<T>())
			{
				a.Tell(msg);
				//flag = true; //если указали актора, то этого достаточно
			}

//			// T - это не актор, ищем акторов, которые следят за UMDViewModel  
//			if( flag == false && msg is MsgWitnessedBase)
//			{
//				var m = msg as MsgWitnessedBase;
//				foreach(var a in _actors.Values)
//				{
//					if( a.Witnessed.GetHashCode() == m.Witnessed.GetHashCode() )
//					{
//						a.Tell(msg);
//					}
//				}
//			}
		}

		public void Tell(object msg)
		{
			foreach (var a in _actors.Values)
				a.Tell(msg);
		}

		public void Dispose()
		{
			while( _actors.Keys.Count > 0 )
			{
				var k = _actors.Keys.First();
				RemoveActor(k);
			}
			
			Debug.WriteLine("Все акторы удалены");
		}

		public int ActorCount{
			get{
				return Instance._actors.Count;
			}
		}

		public ActorBase this[string aKey]
		{
			get{
				if( !_actors.ContainsKey(aKey) )
					Debug.WriteLine("Актор " + aKey + " не найден");
				return _actors[aKey];
			}
		}
	}
}
