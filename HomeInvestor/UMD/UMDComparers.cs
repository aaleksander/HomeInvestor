using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMD
{
    public class UMDComparerByName<T> : IComparer<T>
        where T : UMDViewModelBase
    {
        public int Compare(T a, T b)
        {
            return a.Name.CompareTo(b.Name);//, StringComparison.Ordinal);
        }
    }
}
