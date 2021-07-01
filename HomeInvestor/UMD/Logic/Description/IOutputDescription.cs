using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMD.Logic.Description
{
    /// <summary>
    /// интерфейс для объектов, которые могут быть выходом (класс(default-выход) или выход класса)
    /// </summary>
    public interface IOutputDescription
    {
        bool IsDefault { set; get; }
        UMDClass Class{ get;}
    }
}
