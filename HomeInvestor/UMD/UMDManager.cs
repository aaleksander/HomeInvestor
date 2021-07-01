using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using HomeInvestor.Actors;

namespace UMD
{
	public interface IUMDManager
	{
		void AppendCache(UMDViewModelBase aObj);
		void AppendCache(UMDPropertyViewModel aProp);
		void Remove(UMDViewModelBase aObj);
		void Remove(UMDPropertyViewModel aProp);
		void FillCache();
	}

	/// <summary>
	/// Отвечает за кэширование, создание объектов и прочее
	/// </summary>
	public class UMDManager: IUMDManager
	{
		private UMDManager()
		{
			if(UMDManager.Storage == null )
				throw new Exception("Перед обращением к UMDManager в первый раз необходимо указать Storage");
			
			Ready = false;
			Loading = new object();
		}

		public static UMDManager Instance{
			get{
				if( _inst == null )
					_inst = new UMDManager();
				return _inst;
			}
		}
		private static UMDManager _inst = null;

		public static IStorage Storage{
			set{
				_storage = value;
			}
			get{
				return _storage;
			}
		}
		private static IStorage _storage = null;

		/// <summary>
		/// Добавляет объект-заглушку, которая хранится только в памяти (используется для выпадающих списков)
		/// </summary>
		/// <param name="aKey"></param>
		/// <param name="aName"></param>
		/// <returns></returns>
		public UMDManager AddStub<T>(int aKey, string aName, Action<T> aAction = null) where T:UMDViewModelBase
		{
			var t = typeof(T);
			var o =  (UMDViewModelBase)Activator.CreateInstance(t, (IStorage) null);
			o.Id = aKey;
			o.PP = aKey;
			o.Name = aName;
			o.NoClear = true;

		    if (aAction != null)
		        aAction((T)o);

			AppendCache(o);
			return this;
		}

	    public UMDManager AddStub<T>(int aKey, int aParentId, string aName, T aObj) where T : UMDViewModelBase
        {
	        aObj.Id = aKey;
	        aObj.ParentId = aParentId;
	        aObj.PP = aKey;
	        aObj.NoClear = true;
	        aObj.Name = aName;

            AppendCache(aObj);
	        return this;
        }
		
		public T CreateStub<T>(int aKey, string aName)
		{
			var t = typeof(T);
			var o =  (UMDViewModelBase)Activator.CreateInstance(t, (IStorage) null);
			o.Id = aKey;
			o.PP = aKey;
			o.Name = aName;
			o.NoClear = true;	

			return (T)(object)o;
		}

		/// <summary>
		/// положить объект в кэш
		/// </summary>
		/// <param name="aObj"></param>
		public void AppendCache(UMDViewModelBase aObj)
		{
			if( _objCache.ContainsKey(aObj.GetType()) == false )
			{
				_objCache[aObj.GetType()] = new Collection<UMDViewModelBase>();
			}
			_objCache[aObj.GetType()].Add(aObj);
		}

		/// <summary>
		/// Удалить объект из кэша
		/// </summary>
		/// <param name="aObj"></param>
		public void Remove(UMDViewModelBase aObj)
		{
			if( _objCache.ContainsKey(aObj.GetType()) == false )
				return;
			_objCache[aObj.GetType()].Remove(aObj);
		}

		/// <summary>
		/// положить свойство в кэш
		/// </summary>
		/// <param name="aProp"></param>
		public void AppendCache(UMDPropertyViewModel aProp)
		{
			//_propCache.Add(aProp);
			if( _propCache.ContainsKey(aProp.ObjectId) == false ) //такого ключа еще нет
			{
				_propCache[aProp.ObjectId] = new Collection<UMDPropertyViewModel>();
			}
			_propCache[aProp.ObjectId].Add(aProp);
		}

		public void Remove(UMDPropertyViewModel aProp)
		{
			_propCache[aProp.ObjectId].Remove(aProp);
		}

        /// <summary>
        /// чистим не связанные объекты
        /// </summary>
	    public void Clear()
	    {
            Storage.BeginTransaction();

            bool deleted = true;

            while (deleted)
            {
                deleted = false;
                var l = Storage.Select<UMDObjectModel>("ParentId <> 0", ""); //объекты, у которых должны быть родители
                //var sb = new StringBuilder();
                var cnt = 0;
                foreach (var i in l)
                {
                	if (Storage.Select<UMDObjectModel>("Id=" + i.ParentId.ToString(), "").AsParallel().Any() == false) //родители не найдены
                    {
                        Storage.Delete(i);
                        cnt++;
                        //sb.AppendLine($"deleted object {i.Name}");
                        //Debug.WriteLine("deleted object " + i.Name);
                        deleted = true;
                        //удаляем свойства
                        var ll = Storage.Select<UMDPropertyModel>("ObjectId=" + i.Id.ToString(), ""); //свойсва, у которых есть объекты
                        foreach (var j in ll)
                        {
                            Storage.Delete(i);
                            cnt++;
                            //sb.AppendLine($"  deleted property {j.PropertyName}");
                            //Debug.WriteLine($"  deleted property {j.PropertyName}");
                        }

                        //if (sb.Length > 3000)
                        //{
                        //    Debug.WriteLine(sb.ToString());
                        //    sb.Clear();
                        //}
                        //удаляем свойтва у этого объекта
                    }
                }

                //Debug.WriteLine($"deleted: {cnt}");

                //чистим лишние свойства
                //var lll = Storage.Select<UMDPropertyModel>("", ""); //свойсва, у которых есть объекты
                //foreach (var i in lll)
                //{
                //    if (Storage.Select<UMDPropertyModel>($"ObjectId={i.ObjectId}", "").AsParallel().Any() == false)
                //    {
                //        Storage.Delete(i);
                //        Debug.WriteLine($"deleted property {i.PropertyName}");
                //        deleted = true;
                //    }
                //}
            }
            Storage.Commit();
        }

        /// <summary>
        /// сообщает о том, что кэш загружен и все готово к работе
        /// </summary>
	    public bool Ready { private set; get; }

	    public object Loading { set; get; }

	    /// <summary>
	    /// прочитать файл в кэш
	    /// </summary>
	    public void FillCache()
	    {
	        lock (Loading)
	        {
	            Ready = false;
	            //чистим непривязанные объекты
	            //Clear();

	            //чистим кэш объектов (только те, где NoClear == false
	            foreach (var k in _objCache.Keys)
	            {
	                //чтобы изменить коллекцию в цикле, делаем такой изврат
	                while (_objCache[k].Select(x => x.NoClear == false).Count() == 0)
	                    //пока есть объекты, которые не надо чистить
	                {
	                    foreach (var i in _objCache[k])
	                    {
	                        if (i.NoClear == false)
	                        {
	                            _propCache.Remove(i.Id); //удаляем свойства
	                            _objCache[k].Remove(i);
	                            break;
	                        }
	                    }
	                }
	            }

//	        _propCache.Clear();

	            //вначале кэшшируем свойства
	            var op = Storage.Select<UMDPropertyModel>("", "");
	            foreach (var tmp in op)
	            {
	                AppendCache(new UMDPropertyViewModel(Storage, tmp));
	            }

	            List<string> skip = new List<string>();

	            //создаем полноценные объекты из моделей
	            var oq = Storage.Select<UMDObjectModel>("", "PP");
	            var cnt = 0;
	            var max = oq.Count();
	            foreach (var o in oq)
	            {
	                if (_creators.ContainsKey(o.Alias) == false) //ищем конструктор
	                {
	                    var res = skip.Find(x => x == o.Alias);
	                    if (skip.Find(x => x == o.Alias) == null)
	                    {
	                        skip.Add(o.Alias);
	                        //throw new Exception("Не найден констуктор для " + o.Alias);
	                        Debug.WriteLine("Не найден констуктор для " + o.Alias);
	                    }
	                    continue;
	                }

	                var ob = _creators[o.Alias](Storage, o);//создаем объект
	                if (ob != null)
	                {
	                    AppendCache(ob);
	                }

	                cnt++;

	                if( cnt % 10 == 0 )
	                {
	                	//сообщаем актору о прогрессе загрузки
	                	ActorSystem.Instance[names.progress].Tell(new MsgProgress(((double)cnt)/((double)max)));
	                	//ожидание пока очередь задач не закончится
	                	ActorSystem.Instance[names.progress].Wait();
	                }
	            }

	            //ActorSystem.Instance[names.progress].Tell(new MsgProgress
	            Debug.WriteLine("оптимизация");
	            //убираем "зависшие" позиции
//	            _storage.BeginTransaction();
//	            foreach(var pp in UMDQuery.Select<PositionViewModel>())
//	            {
//	            	if( pp.Portfolio == null )
//	            		pp.Delete();
//	            }
//	            _storage.Commit();

	            Ready = true;
	        }
	    }

	    Dictionary<Type, Collection<UMDViewModelBase>> _objCache = new Dictionary<Type, Collection<UMDViewModelBase>>();

		//ключ - это идешник объекта
		Dictionary<int, Collection<UMDPropertyViewModel>> _propCache = new Dictionary<int, Collection<UMDPropertyViewModel>>();

        /// <summary>
        /// Регистрируем кострукторы
        /// </summary>
	    public static void RegCreators()
	    {
            //ищем классы, унаследованные от UMDViewModelBase и помеченные атрибутом Alias
            Type ourType = typeof(UMDViewModelBase);
            var list = Assembly.GetAssembly(ourType).GetTypes()
                .Where(type => type.IsSubclassOf(ourType))
                .Where(x => x.GetCustomAttribute(typeof(AliasAttribute), false) != null);
            //генерируем для них конструкторы
            foreach (var t in list)
            {
                var tmp = t.GetCustomAttribute(typeof(AliasAttribute), false);
                var attr = (AliasAttribute)tmp;
                Func<IStorage, UMDObjectModel, UMDViewModelBase> func
                    = (s, a) =>
                    (UMDViewModelBase)Activator.CreateInstance(t, s, a);
                _creators[attr.Alias] = func;
            }
        }

		/// <summary>
		/// Зарегистрировать конструктор
		/// </summary>
		/// <param name="aAlias"></param>
		/// <param name="aFun"></param>
		//public static void RegCreator(string aAlias, Func<IStorage, UMDObjectModel, UMDViewModelBase> aFun)
		//{
		//	_creators[aAlias] = aFun;
		//}

		static Dictionary<string, Func<IStorage, UMDObjectModel, UMDViewModelBase>> _creators 
			= new Dictionary<string, Func<IStorage, UMDObjectModel, UMDViewModelBase>>();

		/// <summary>
		/// ищем свойство объекта по номеру
		/// </summary>
		/// <param name="aObjId"></param>
		/// <param name="aPropPP"></param>
		/// <returns></returns>
		public UMDPropertyViewModel GetPropertyForObject(int aObjId, int aPropPP)
		{
			UMDPropertyViewModel res = null;
			if( _propCache.ContainsKey(aObjId) == false )
				return null;
			res = _propCache[aObjId].FirstOrDefault(x => x.PPInObject == aPropPP);

			return res;
		}

		/// <summary>
		/// получить объект по идентификатору
		/// </summary>
		/// <param name="aId"></param>
		/// <returns></returns>
		public UMDViewModelBase GetObjectById<T>(int aId)
		{
			try
			{
				var t = typeof(T);
				var res = _objCache[t].First(x => x.Id == aId);
				return res;
			}catch(InvalidOperationException)
			{
				throw new Exception(typeof(T).Name + " с Id=" + aId.ToString() + " не найден");
			}
		}

        public bool Any<T>(Func<T, bool> aPref ) where T: UMDViewModelBase
        {
            var t = typeof(T);
            if (!_objCache.ContainsKey(t))
                return false;

            return _objCache[t].Any(o => aPref((T) (object) o));
        }

        public T First<T>(Func<T, bool> aPref)
        {
            var t = typeof(T);

            foreach (var o in _objCache[t])
            {
                if (aPref((T)(object)o))
                    return (T)(object)o;
            }
            throw new Exception("Ничего не найдено");
        }

        /// <summary>
        /// возвращает истину, если существует объект такого типа с таким идешником
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        public bool CheckObjectById<T>(int aId)
		{
			try
			{
				var t = typeof(T);
				var res = _objCache[t].First(x => /*x is T && */x.Id == aId);
				return true;
			}catch(InvalidOperationException)
			{
				return false;
			}catch(KeyNotFoundException)
			{
				return false;
			}
						
		}

		/// <summary>
		/// возвращает первый попавшийся объект определенного типа
		/// </summary>
		/// <returns></returns>
		public T GetFirstObject<T>()
		{
			try
			{
				var t = typeof(T);
				var res = (T)(object)_objCache[t].First();
				//T res = (T)(object)_objCache.First(x => x is T);
				return res;
			}catch(InvalidOperationException)
			{
				throw new UMDNoObjectException("Ни одного объекта " + typeof(T).Name + " не найдено");
			}catch(KeyNotFoundException)
			{
				throw new UMDNoObjectException("Ни одного объекта " + typeof(T).Name + " не найдено");
			}
		}

		/// <summary>
		/// возвращает первый попавшийся объект определенного типа
		/// </summary>
		/// <returns></returns>
		public T GetFirstObject<T>(Func<T, bool> aFilter)
		{
			foreach(var o in _objCache)
			{
				if( o is T )
				{
					if( aFilter((T)(object)o) )
						return (T)(object)o;
				}
			}
			throw new UMDNoObjectException("Объект " + typeof(T).Name + " не найден");
		}		
		
		/// <summary>
		/// взять объекты определенного типа
		/// </summary>
		/// <param name="aOrderField"></param>
		/// <returns></returns>
		public ObservableCollection<T> GetList<T>()
		{
			ObservableCollection<T> res = new ObservableCollection<T>();

			var t = typeof(T);
			
			if( _objCache.ContainsKey(t) == false )
				return res;

			foreach(var o in _objCache[t])
			{
				res.Add((T)(object)o);
			}
			return res;
		}
		
		public ObservableCollection<UMDViewModelBase> GetListByType(Type aType)
		{
			ObservableCollection<UMDViewModelBase> res = new ObservableCollection<UMDViewModelBase>();

			var t = typeof(UMDViewModelBase);
			
			if( _objCache.ContainsKey(t) == false )
				return res;

			foreach(var o in _objCache[t])
			{
				res.Add((UMDViewModelBase)(object)o);
			}
			return res;
		}
		
		public ObservableCollection<T> GetList<T>(Func<T, bool> aFilter)
		{
			ObservableCollection<T> res = new ObservableCollection<T>();

			var t = typeof(T);

			if( _objCache.ContainsKey(t) == false )
				return res;

			foreach(var o in _objCache[t])
			{
				if( aFilter((T)(object)o) )
					res.Add((T)(object)o);
			}
			return res;
		}
	}
}
