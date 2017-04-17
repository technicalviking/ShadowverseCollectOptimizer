using CollectionRate2.Lib.SetBreakdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.Players
{
    public interface IPlayer
    {
        void InitPlayer();
        void AddCollection(IBreakdown targetSet);
        void Collect();
        string name { get; set; }
        Dictionary<string, int> numPacks { get; set; }

    }
}
