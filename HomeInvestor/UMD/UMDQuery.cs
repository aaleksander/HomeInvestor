using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UMD
{
	/// <summary>
	/// класс для создания запросов к универсальному хранилищу
	/// </summary>
	public class UMDQuery
	{
		public UMDQuery()
		{
		}

        public static ParallelQuery<T> Select<T>()
		{
			return UMDManager.Instance.GetList<T>().AsParallel<T>();
		}

		public static T ById<T>(int aId)
		{
			return (T)(object)UMDManager.Instance.GetObjectById<T>(aId);
		}

        public static T ById<T>(int aId, T def) where T:UMDViewModelBase
        {
            if( UMDManager.Instance.Any<T>(x => x.Id == aId ))
                return (T)(object)UMDManager.Instance.GetObjectById<T>(aId);
            return def;
        }

        public static bool Any<T>(Func<T, bool> aF) where T: UMDViewModelBase
	    {
	        return UMDManager.Instance.Any<T>(aF);
	    }

        public static T First<T>(Func<T, bool> aF, T def = null) where T:UMDViewModelBase
        {
            if( UMDManager.Instance.Any<T>(aF) )
                return UMDManager.Instance.First<T>(aF);
            return def;
        }

        public static T First<T>(T def = null) where T : UMDViewModelBase
        {
            if (UMDManager.Instance.Any<T>(x => true))
                return UMDManager.Instance.First<T>(x => true);
            return def;
        }
    }
	
	public static class ObservableExt
	{
		/// <summary>
		/// преобразовать в ObservableCollection
		/// </summary>
		/// <param name="col"></param>
		/// <returns></returns>
		public static ObservableCollection<T> ToOC<T>(this IEnumerable<T> col)
		{
			var res = new ObservableCollection<T>();
			foreach(var r in col)
			{
				res.Add(r);
			}
			return res;
		}
	}
}
