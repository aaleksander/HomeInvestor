using System;
using PetaPoco;

namespace UMD
{
	/// <summary>
	/// модель для объектов
	/// </summary>
	[TableNameAttribute("UMDObject")]
	[PrimaryKey("Id", AutoIncrement = true)]	
	public class UMDObjectModel: ModelBase
	{
		public int Id{set;get;}
		public int ParentId{set;get;}
		public string Alias{set;get;}
		public int PP{set;get;}
		public string Name{set;get;}
//        public int AlterParentId { set; get; }
//        public int AlterPP { set; get; }
	}

	/// <summary>
	/// модель для свойства
	/// </summary>
	[TableName("UMDProperty")]
	[PrimaryKey("Id", AutoIncrement = true)]	
	public class UMDPropertyModel: ModelBase
	{
		public int Id{set;get;}
		public int ObjectId{set;get;}
		public int PPInObject{set;get;}
		public string PropertyName{set;get;}
		public string Type{set;get;}
		public int IntValue{set;get;}
		public string StrValue{set;get;}
		public double RealValue{set;get;}
	}
}
