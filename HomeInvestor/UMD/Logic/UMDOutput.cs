using System;
using UMD.Logic.Description;

namespace UMD.Logic
{
	/// <summary>
	/// Выход конкретного объекта
	/// </summary>
	public class UMDOutput: UMDConnectorBase
    {
		public string Alias{set;get;}
        public bool IsDefault { set; get; }
		//public UMDOutputSlotDescr Source{private set;get;}
		//public UMDObject Parent{set;get;}
		//public UMDConnection Connection{set;get;}

		public UMDOutput(UMDSlotDescription aSlot, string aName, string aDesciption)
            :base(aSlot, aName, aDesciption)//UMDOutputSlotDescr a, UMDObject aParent)
		{
			//Alias = a.Name;
			//Description = a.Description;
			//Source = a;
			//Parent = aParent;
			//Connection = null;
		}
	}
}
