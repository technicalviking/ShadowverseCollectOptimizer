using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.SetBreakdowns
{
    public class BahamutBreakdown: IBreakdown
    {
        public string NAME { set { } get { return "BAHAMUT"; } }

        public int BRONZE { set { } get { return 40; } }
        public int SILVER { set { } get { return 32; } }
        public int GOLD { set { } get { return 24; } }
        public int LEGENDARY { set { } get { return 9; } }
    }
}
