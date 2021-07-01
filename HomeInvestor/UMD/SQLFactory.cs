using System;
using System.Globalization;
using System.Runtime.Serialization;
using PetaPoco;

namespace UMD
{
	[Serializable]
	public class NoTableNameAttributeException: Exception
	{
		public NoTableNameAttributeException(SerializationInfo si, StreamingContext sc):base(si, sc){}
		public NoTableNameAttributeException():base(){}
		public NoTableNameAttributeException(String aMessage):base(aMessage){}
		public NoTableNameAttributeException(String str, Exception ex):base(str, ex){}
	}

	/// <summary>
	/// Description of SQLFactory.
	/// </summary>
	public class SQLFactory
	{
		protected static string TableName<T>()
		{
			var tt = typeof(T);
			System.Attribute[] attrs = (System.Attribute[])typeof(T).GetCustomAttributes( typeof(TableNameAttribute), false );

			string tableName = ((TableNameAttribute) attrs[0]).Value;

            if( tableName == "" )
            {
            	throw new NoTableNameAttributeException();
            }

            return tableName;
		}
		
        public static Sql Select<T>()
        {            
            return Sql.Builder
                .Select("*")
            	.From(TableName<T>());
        }

        public static Sql SelectByID<T>(int aID)
        {
            return
                Sql.Builder
				.Select("*")
				.From(TableName<T>())
				.Where("Id = " + aID.ToString(CultureInfo.CurrentCulture)); //надо бы найти в рефлекшене найти поле, которое является идешником
        }

        public static Sql Delete<T>()
        {
        	return new Sql("Delete")
        		.From(TableName<T>());
        }        
	}
}
