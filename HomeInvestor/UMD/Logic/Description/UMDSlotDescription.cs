using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMD.Logic.Description
{
    /// <summary>
    /// описание слота класса (вход или выход)
    /// </summary>
    public class UMDSlotDescription: IOutputDescription
    {
        public string Name { private set; get; }
        public string Descr { private set; get; }
        public UMDClass Class{ private set; get;}
        public bool IsDefault { set; get; }
        public UMDSlotDescription(string aName, string aDescr, UMDClass aParent)
        {
            Name = aName;
            Descr = aDescr;
            Class = aParent;
        }

        /// <summary>
        /// добавляем слот, с которым это слот может коннектиться
        /// </summary>
        /// <param name="aSlot"></param>
        public void AddPermission(IOutputDescription aSlot)
        {
            _slot.Add(aSlot);
        }
        private List<IOutputDescription>  _slot = new List<IOutputDescription>();

        /// <summary>
        /// возвращаем истину, если может приконектиться к этому слоту
        /// </summary>
        /// <param name="aSlot"></param>
        /// <returns></returns>
        public bool CanConnect(IOutputDescription aSlot)
        {
            if ( _slot.Count == 0 )//нет ни одного правила, наверное это выход, пусте конектится кто хочет
                return true;

            if(_slot.FirstOrDefault(x => x == aSlot) != null) //проверим входы/выходы "как есть"
            	return true;

            if(_slot.FirstOrDefault(x => x == aSlot.Class) != null) //если в качестве возможного входа указан целый класс.
            	return true;
            
            return false;
        }
    }
}
