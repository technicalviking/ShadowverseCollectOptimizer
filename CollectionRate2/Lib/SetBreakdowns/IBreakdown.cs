using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.SetBreakdowns
{
    public interface IBreakdown
    {
        string NAME { get; set; }

        int BRONZE { get; set; }
        int SILVER { get; set; }
        int GOLD { get; set; }
        int LEGENDARY { get; set; }

    }
}
