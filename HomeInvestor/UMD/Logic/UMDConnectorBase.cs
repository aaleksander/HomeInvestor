using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMD.Logic.Description;

namespace UMD.Logic
{
    /// <summary>
    /// базовый класс для всего, что может быть коннектором (для объекта, не для класса
    /// </summary>
    public class UMDConnectorBase
    {
        public UMDConnectorBase(UMDSlotDescription aSlot, string aName, string aDescription)
        {
            Name = aName;
            Description = aDescription;
            Spec = aSlot;
        }

        public string Name { private set; get; }
        public string Description { private set; get; }

        /// <summary>
        /// описание коннектора
        /// </summary>
        public UMDSlotDescription Spec { private set; get; }
    }
}
