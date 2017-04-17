using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.RarityStrategies
{
    public class LowToHighBySet : IRarityStrategy
    {
        List<Rarities> Priority = new List<Rarities>()
        {
            Rarities.Bronze,
            Rarities.Silver,
            Rarities.Gold,
            Rarities.Legendary
        };

        public List<Rarities> GetRarityPriority(Collection curCollection)
        {
            return Priority.Where(p => !curCollection.FullSets[p]).ToList();
        }
    }
}