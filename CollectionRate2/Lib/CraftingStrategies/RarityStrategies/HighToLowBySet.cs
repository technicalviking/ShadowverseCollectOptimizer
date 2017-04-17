using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.RarityStrategies
{
    public class HighToLowBySet: IRarityStrategy
    {
        List<Rarities> Priority = new List<Rarities>()
        {
            Rarities.Legendary,
            Rarities.Gold,
            Rarities.Silver,
            Rarities.Bronze
        };

        public List<Rarities> GetRarityPriority(Collection curCollection)
        {
            return Priority.Where(p => !curCollection.FullSets[p]).ToList();
        }
    }
}
