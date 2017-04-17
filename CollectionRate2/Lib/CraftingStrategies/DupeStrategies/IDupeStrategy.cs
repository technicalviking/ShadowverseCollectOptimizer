using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.DupeStrategies
{
    public  interface IDupeStrategy
    {
        int GetPriorityIndex(Collection curCollection, Rarities targetRarity);
    }
}
