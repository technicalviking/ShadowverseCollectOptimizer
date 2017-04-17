using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.SetBreakdowns
{
    public class BaseSetBreakdown : IBreakdown
    {
        public string NAME { set { } get { return "BASE SET"; } }

        public int BRONZE { set { } get { return 126; } }
        public int SILVER { set { } get { return 99; } }
        public int GOLD { set { } get { return 69; } }
        public int LEGENDARY { set { } get { return 24; } }
    }
}
