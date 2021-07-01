using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMD.Logic.Description;

namespace UMD.Logic
{
    public class UMDObject
    {
        public UMDObject(UMDClass aClass, string aName, string aDescription)
        {
            Name = aName;
            Description = Description;
            Class = aClass;

            CreateInputs(aClass.Inputs);
            CreateOutputs(aClass.Outputs);
        }

        public string Name { private set; get; }
        public string Description { private set; get; }

        public UMDClass Class { private set; get; }


        public List<UMDOutput> Outputs { 
        	get{
        		return _outputs;
        	}
        }
        List<UMDOutput> _outputs = new List<UMDOutput>();

        public List<UMDInput> Inputs { 
        	get{
        		return _inputs;
        	}
        }
        List<UMDInput> _inputs = new List<UMDInput>();

        void CreateInputs(List<UMDSlotDescription> aList)
        {
            foreach (var i in aList)
            {
                var newI = new UMDInput(i, i.Name, i.Descr);
                Inputs.Add(newI);
            }
        }

        private void CreateOutputs(List<UMDSlotDescription> aList)
        {
            foreach (var i in aList)
            {
                var newI = new UMDOutput(i, i.Name, i.Descr);
                Outputs.Add(newI);
            }
        }
    }
}
