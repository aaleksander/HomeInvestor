using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMD.Logic.Description;

namespace UMD.Logic
{
    public class UMDInput: UMDConnectorBase
    {
        public UMDInput(UMDSlotDescription aSlot, string aName, string aDescr) : base(aSlot, aName, aDescr)
        {
        }        

    }
}
