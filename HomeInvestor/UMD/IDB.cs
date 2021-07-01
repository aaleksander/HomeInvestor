using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using PetaPoco;

namespace UMD
{
	/// <summary>
	/// База данных
	/// </summary>
	public interface IStorage
	{
		int Insert(ModelBase a);
		void Update(ModelBase a);
		void Delete(ModelBase a);
		IEnumerable<T> Select<T>(string aWhere, string aOrder);

		string FileName{set;get;}

		void BeginTransaction();
		void Commit();
		void Rollback();

		void Close();
	}

	public class Storage: IStorage
	{
		public int Insert(ModelBase a)
		{
//			if( a is UMDObjectModel )
//				Debug.WriteLine("ddd");
//			Debug.WriteLine("insert " + a.ToString());
			int res = 0;

			res = Convert.ToInt32(_db.Insert(a));

			return res;
		}

		public void Delete(ModelBase a)
		{
			Debug.WriteLine("delete");
            _db.Delete(a);
		}

		public void Update(ModelBase a)
		{			
		    lock (UMDManager.Instance.Loading)
		    {
		        //Debug.WriteLine("update");
		        _db.Update(a);
		    }
		}

		public void Close()
		{
            //Debug.WriteLine("close");
            _db.Dispose();
		}

		/// <summary>
		/// выбирает из кэша объекты
		/// </summary>
		/// <param name="aFilter"></param>
		/// <param name="aOrder"></param>
		/// <returns></returns>
		public IEnumerable<T> Select<T>(string aWhere, string aOrder)
		{
			Sql s = SQLFactory.Select<T>();
			if( aWhere != "" )
				s = s.Where(aWhere);

			if( aOrder != "" )
				s = s.OrderBy(aOrder);
			return _db.Query<T>(s);
		}

		private Database _db = null;

		public string FileName{
			set{
                //Debug.WriteLine("set file name");
                DB.FileName = value;
				_db = new DB();
			}
			get{
				return DB.FileName;
			}
		}

		private List<UMDObjectModel> _cacheObj = new List<UMDObjectModel>();
		private List<UMDPropertyModel> _cacheProp = new List<UMDPropertyModel>();

		public override string ToString()
		{
			return "SQLite Storage!";
		}

        public bool IsTransaction { private set; get; }
		public void BeginTransaction()
		{
		    lock (UMDManager.Instance.Loading)
		    {		        
		        if( !IsTransaction )
		        {
		        	Debug.WriteLine("begin transaction");
		        	_db.BeginTransaction();
		        	IsTransaction = true;
		        }
		    }
		}

		public void Commit()
		{
		    lock (UMDManager.Instance.Loading)
		    {
		    	if( IsTransaction )
		    	{
		        	Debug.WriteLine("commit");
		        	_db.CompleteTransaction();
		        	IsTransaction = false;
		    	}
		    }
		}

		public void Rollback()
		{
		    lock (UMDManager.Instance.Loading)
		    {
		    	if( IsTransaction )
		    	{
		        	Debug.WriteLine("rollback");
		        	_db.AbortTransaction();
		        	IsTransaction = false;
		    	}
		    }
		}
	}
}