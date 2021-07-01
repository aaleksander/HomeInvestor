using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace HomeInvestor.Actors
{
	public delegate void OnAction(object sender);

	public class ActorBase: IDisposable
	{
		public string Name{set;get;}

		EventWaitHandle wh = new AutoResetEvent(false);
		Thread _worker = null;
		object locker = new object();
		Queue<Tuple<object, Action<object>>>  tasks = new Queue<Tuple<object, Action<object>>>();

		public int TaskCount{
			get{
				return tasks.Count;
			}
		}

		public delegate void OnEventContainer();
		public object Witnessed{private set;get;}
		public int WitnessedHashCode{
			get{
				if( Witnessed == null )
					return 0;
				return Witnessed.GetHashCode();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="aName">название актора (уникальное в системе)</param>
		/// <param name="witnessed">некий наблюдаемый объект</param>
		/// <param name="noThread">должен ли он выполняться в отдельном потоке</param>
		public ActorBase(string aName, object witnessed, bool noThread = false)
		{
			Witnessed = witnessed;

			Name = aName;
			if( !noThread ) //в отдельном потоке
			{
				//Debug.WriteLine(Name + ": стартуем отдельный поток");
				_worker = new Thread(Work);
				_worker.Priority = ThreadPriority.Lowest;
				_worker.Start();
			}

			ActorSystem.Instance.AddActor(this);
		}

		public virtual void Tell(object aMsg, Action<object> callback = null )
		{
			if( _worker == null ) //однозадачный
			{
				var res = OnReceive(aMsg);
				if( callback != null )
					callback(res);
			}
			else
			{
				lock(locker)
				{
					//Debug.WriteLine(Name + ": добавляем задачу в очередь");
					tasks.Enqueue(Tuple.Create(aMsg, callback));
				}
				wh.Set();
			}
		}

		public void Do(Action act)
		{
			new Thread(() => act()).Start();
		}

		public void Dispose()
		{
//			DebugPrint("Dispose");
			
			if( _worker != null )
			{
				_worker.Abort();
//			_worker.Join();
				wh.Close();
			}
//			ActorSystem.Instance.RemoveActor(this);
			//DebugPrint("Disposed");
		}

		private void Work()
		{
			try
			{
				while(true)
				{
					Tuple<object, Action<object>> task = null;

					lock(locker)
					{
						if( tasks.Count > 0 )
						{
							task = tasks.Dequeue();
							if( task == null )
								return;
						}
					}

					if( task != null )
					{
						//Debug.WriteLine(Name + ": выполняем задачу из очереди");
						var res = OnReceive(task.Item1);
						if(task.Item2 != null )
						{
							task.Item2.Invoke(res);
						}
					}
					else
					{
						wh.WaitOne();
					}
					Thread.Sleep(30);
				}
			}catch(ThreadAbortException)
			{
				
			}
		}

		protected virtual object OnReceive(object aMsg)
		{
			return null;
		}

		/// <summary>
		/// возвращает истину, если в очереди есть сообщение, удовлетворяющее aPredicate
		/// </summary>
		/// <param name="aPreficate"></param>
		/// <returns></returns>
		public bool CheckTask(Func<object, bool> aPredicate)
		{
			lock(locker)
			{
				foreach(var a in tasks)
				{
					if( aPredicate(a) )
						return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// ждем, пока не закончится очередь задач
		/// </summary>
		public void Wait()
		{
			while( TaskCount > 0 )
				Thread.Sleep(10);
		}

		protected void DebugPrint(string aText)
		{
			Debug.WriteLine(Name + " (" + tasks.Count.ToString() + "):\t" + aText);
		}
	}
}
