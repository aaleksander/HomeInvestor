using System;
using System.Data.SQLite;
using PetaPoco;

namespace UMD
{
	public class DB: Database
	{
		public static string FileName{
			set{
				_filename = value;
			}
			get{
				return _filename;
			}
		}
		private static string _filename = null;
		
		private SQLiteConnection _conn = null;

		public void Close()
		{
			_conn.Close();
			//все чистим, чтобы дескриптор файла отпустило
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public new static string ConnectionString{ get{ return @"Data Source=" + FileName + ";Version=3;";}}

	    public DB():base(ConnectionString, "System.Data.SQLite")
		{
//			var res = Execute("pragma cache_size=8000;");
		}
//		public DB():base(@"Data Source=:memory:;Version=3;", "System.Data.SQLite"){}

		public DB(string aConnectionStr, string aProvider): base(aConnectionStr, aProvider){}

        public T SelectByID<T>(int aId)
        {
        	return Single<T>(SQLFactory.SelectByID<T>(aId));
        }
	}
}
