using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.RarityStrategies
{
    public interface IRarityStrategy
    {
        List<Rarities> GetRarityPriority(Collection curCollection);
    }
}
