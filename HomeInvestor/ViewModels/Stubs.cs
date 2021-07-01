using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	public class StubBase: UMDViewModelBase
	{
		public StubBase(IStorage aSt): base(aSt)
		{
			RealTimeUpdate = false;
		}
		public StubBase(IStorage aSt, UMDObjectModel aModel): base(aSt, aModel)
		{
			RealTimeUpdate = false;
		}

	    public override string ToString()
	    {
	        return Name;
	    }
	}

    [Alias("obl_type")]
    [Properties()]
    public class OblType_Stub : StubBase
    {
        public OblType_Stub(IStorage aSt) : base(aSt) { }
        public OblType_Stub(IStorage aSt, UMDObjectModel aModel) : base(aSt, aModel) { }
    }
}
