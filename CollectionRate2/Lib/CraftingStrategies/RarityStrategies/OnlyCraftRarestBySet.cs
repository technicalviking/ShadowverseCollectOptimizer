using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.RarityStrategies
{
    public class OnlyCraftRarestBySet : IRarityStrategy
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
            return new List<Rarities>(){
                Priority.First(p => !curCollection.FullSets[p])
            };
        }
    }
}
