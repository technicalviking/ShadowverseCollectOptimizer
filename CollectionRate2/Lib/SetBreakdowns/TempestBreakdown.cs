using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.SetBreakdowns
{
    public class TempestBreakdown : IBreakdown
    {
        public string NAME { set { } get { return "TEMPEST"; } }

        public int BRONZE { set { } get { return 32; } }
        public int SILVER { set { } get { return 32; } }
        public int GOLD { set { } get { return 24; } }
        public int LEGENDARY { set { } get { return 16; } }
    }
}
